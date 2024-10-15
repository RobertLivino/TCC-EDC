using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    private bool lookUp;
    private bool lookDown;
    private float moveSpeed = 12f;
    private float gravity = -9.81f;
    private float x;
    private float y;

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

    Vector3 velocity;
    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    private float jumpHeight = 3f;

    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        healthBar.FillHealthStart(5f);
        manaBar.FillManaStart(50f);
        spellDamage = 2f;
        swordDamage = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        //lookUp = y > 0 ? true : false;
        //lookDown = y < 0 ? true : false;

        MovePlayer();

        if (isPlayerInAction() && !knockUpCountdown)
        {
            RotatePlayer();
        }

        if (Input.GetButtonDown("Jump"))
        {
            JumpPlayer();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerAttack();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerCastSpell();
        }

        if (knockUpCountdown)
        {
            UpdateKnockUpCountdown();
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

        if (isGrounded && velocity.y < 0)
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
        if (other.gameObject.tag == "EnemyCrab" && !attackAnimation)
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
            healthBar.TakeDamage(1);
            if (healthBar.currentHealth <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }

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
        Destroy(SpellObj, 15f);
    }

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
        Vector3 move = new Vector3(x, 0, 0);
        if (!knockUpCountdown)
        {
            controller.Move(move * moveSpeed * Time.deltaTime);
        }

        moveSpeed = isGrounded ? 12f : 10f;
        if (x != 0 && isGrounded)
        {
            animator.SetBool("move", true);
        }
        else
        {
            animator.SetBool("move", false);
        }
    }
    private void JumpPlayer()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
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
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void UpdateKnockUpCountdown()
    {
        if (currentKnockUpCountdown > 0)
        {
            currentKnockUpCountdown -= 1 * Time.deltaTime;
            controller.Move(new Vector3(transform.rotation.y < 0 ? 0.5f : -0.5f, 0, 0) * 12f * Time.deltaTime);
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
}
