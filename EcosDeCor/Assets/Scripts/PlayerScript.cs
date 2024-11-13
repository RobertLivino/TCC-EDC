using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public MapaController mapaController;

    private float moveSpeed = 12f;
    private float x;

    private bool knockUpCountdown = false;
    private float startKnockUpCountdown = 0.5f;
    private float currentKnockUpCountdown = 0.5f;

    public HealthBar healthBar;
    public ManaBar manaBar;
    public PlayerSword playerSword;

    private Animator animator;
    public bool attackAnimation;
    public bool hittedOnce;
    public float swordDamage = 1;

    public GameObject SpellPointCast;
    public Vector3 SpellDestination;
    public GameObject spellToCast;
    public float spellSpeed;
    public float spellDamage = 2;

    public Transform groundCheck;
    private float groundDistance = 0.5f;
    public LayerMask groundMask;
    private bool isGrounded;
    private bool isJumping;
    private float jumpCount;
    private float jumpTime = 0.4f;
    private float jumpMultplier = 1.3f;
    private int jumpPower = 8;
    public float fallMultiplayer = 0.001f;
    public Vector3 vecGravity;

    //public CharacterController controller;
    private Rigidbody rb;
    void Start()
    {
        animator = GetComponent<Animator>();
        healthBar.FillHealthStart(5f);
        manaBar.FillManaStart(50f);
        spellDamage = 2f;
        swordDamage = 1f;
        rb = GetComponent<Rigidbody>();
        vecGravity = new Vector3(0, -Physics.gravity.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");

        MovePlayer();

        if (isPlayerInAction() && !knockUpCountdown)
        {
            RotatePlayer();
        }

        if (Input.GetButtonDown("Jump"))
        {
            JumpPlayer();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerAttack();
        }
        if (Input.GetKeyDown(KeyCode.K) && manaBar.currentMana > 0)
        {
            PlayerCastSpell();
        }

        if (knockUpCountdown)
        {
            UpdateKnockUpCountdown();
        }

        if (mapaController.healthMana)
        {
            HealManaByEnemy(mapaController.healthManaValue);
        }
        if (mapaController.healthHealt)
        {
            HealHealthByEnemy(mapaController.healthHealtValue);
        }
        if (Input.GetKeyDown(KeyCode.W) && !mapaController.blockConversation)
        {
            GuardianConversetion();
        }
        
        if (Input.GetKeyDown(KeyCode.W) && mapaController.blockConversation && !mapaController.startedDialogue)
        {
            mapaController.blockConversationCount++;
            if (mapaController.blockConversationCount > 1)
            {
                mapaController.blockConversationCount = 0;
                mapaController.blockConversation = false;
            }
        }

        UpdateGravity();    
    }

    private void FixedUpdate()
    {
        if (transform.position.z != -1.3)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, -1.3f), transform.rotation);
        }
        attackAnimation = animator.GetBool("Attack");
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded && rb.velocity.y < 0)
        {
            animator.SetInteger("jumpState", 0);
        }
        if (!isGrounded && animator.GetInteger("jumpState") != 2)
        {
            animator.SetInteger("jumpState", 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string otherTag = other.gameObject.tag;
        if ((otherTag == "EnemyCrab" || otherTag == "CollossoArm" || otherTag == "Collosso") && !attackAnimation)
        {
            if (other.transform.position.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            knockUpCountdown = true;
            animator.SetBool("knockBack", true);
            if (otherTag == "EnemyCrab")
            {
                healthBar.TakeDamage(1);
            }
            if (otherTag == "CollossoArm")
            {
                healthBar.TakeDamage(2);
            }
            if (healthBar.currentHealth <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if(otherTag == "Memory")
        {
            mapaController.PlayColectedMemory();
            mapaController.memoryImageStatus = true;
        }
        if(otherTag == "EcoCastle" && otherTag == "EcoDesert")
        {
            mapaController.ecoColectedCount++;
            mapaController.PlayColectedMemory();
        }
        if(otherTag == "Void")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
    //Triggers
    private void OnFinishedAscend()
    {
        animator.SetTrigger("jumpAscendFinished");
    }
    private void OnFinishedAttack()
    {
        animator.SetBool("Attack", false);
    }
    private void OnHitted()
    {
        mapaController.PlayAttackAudio();
        hittedOnce = true;
    }
    private void OnFinishHit()
    {
        hittedOnce = false;
    }
    private void OnFinishSpell()
    {
        animator.SetBool("Spell", false);
    }
    private void OnCastSpell()
    {
        var SpellObj = Instantiate(spellToCast, SpellPointCast.transform.position, Quaternion.identity) as GameObject;
        SpellObj.GetComponent<Rigidbody>().velocity = SpellPointCast.transform.forward.normalized * spellSpeed;
        mapaController.PlaySpellAudio();
        Destroy(SpellObj, 15f);
    }
    private void PlayJumpDescend()
    {
        mapaController.PlayJumpDownAudio();
    }

    //metodos
    private void RotatePlayer()
    {
        if (x < 0 && transform.rotation.y != -90)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        if (x > 0 && transform.rotation.y != 90)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }
    private void MovePlayer()
    {
        if (!knockUpCountdown || mapaController.startedDialogue)
        {
            rb.velocity = new Vector3(x * moveSpeed, rb.velocity.y, 0);
        }
        if (mapaController.startedDialogue)
        {
            x = 0;
        }

        moveSpeed = isGrounded ? 12f : 10f;
        if (x != 0 && isGrounded)
        {
            if (!mapaController.walking) mapaController.PlayWalkAudio();
            animator.SetBool("move", true);
        }
        else
        {
            mapaController.StopWalkAudio();
            animator.SetBool("move", false);
        }
    }
    private void JumpPlayer()
    {
        if (isGrounded)
        {
            isJumping = true;
            jumpCount = 0;
            mapaController.PlayJumpUpAudio();
            rb.velocity = new Vector3(rb.velocity.x, jumpPower, 0);
        }
    }
    private void PlayerAttack()
    {
        if (isPlayerInAction())
        {
            animator.SetBool("Attack", true);
        }
    }
    private void PlayerCastSpell()
    {
        if (isPlayerInAction())
        {
            manaBar.useMana(10f);
            animator.SetBool("Spell", true);
        }
    }
    private void UpdateGravity()
    {
        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCount += Time.deltaTime;
            if (jumpCount > jumpTime) isJumping = false;

            float t = jumpCount / jumpTime;
            float currentjump = jumpMultplier;

            if (t > 0.5)
            {
                currentjump = jumpMultplier * (1 - t);
            }
            rb.velocity += vecGravity * jumpMultplier * Time.deltaTime;
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpCount = 0;

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.6f, 0);
            }
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallMultiplayer * Time.deltaTime;
        }
    }
    private void UpdateKnockUpCountdown()
    {
        if (currentKnockUpCountdown > 0)
        {
            currentKnockUpCountdown -= 1 * Time.deltaTime;
            if (transform.rotation.y < 0)
            {
                rb.velocity = new Vector3(8, rb.velocity.y, 0);
            }
            else
            {
                rb.velocity = new Vector3(-8, rb.velocity.y, 0);
            }
        }
        else
        {
            currentKnockUpCountdown = startKnockUpCountdown;
            knockUpCountdown = false;
            animator.SetBool("knockBack", false);
        }
    }
    private bool isPlayerInAction()
    {
        if(!animator.GetBool("Attack") && !animator.GetBool("Spell"))
        {
            return true;
        }
        return false;
    }
    private void GuardianConversetion()
    {
        if (mapaController.guardianRange && mapaController.guardianDoor && !mapaController.startedDialogue)
        {
            mapaController.startedDialogue = true;
        }
    }
    public void HealManaByEnemy(float heal) 
    {
        mapaController.healthMana = false;
        manaBar.HealMana(heal > manaBar.maxMana ? manaBar.maxMana - manaBar.currentMana : heal);
    }
    public void HealHealthByEnemy(float heal) 
    {
        mapaController.healthHealt = false;
        healthBar.HealDamage(heal > healthBar.maxHealth ? healthBar.maxHealth - healthBar.currentHealth : heal);
    }
}
