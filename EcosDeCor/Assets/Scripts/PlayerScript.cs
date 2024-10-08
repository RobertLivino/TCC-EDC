using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    private bool lookUp;
    private bool lookDown;
    private float moveSpeed = 12f;
    private float gravity = -9.81f;
    private float x;
    private float y;

    private bool knockUpCountdown = false;
    private float startKnockUpCountdown = 2f;
    private float currentKnockUpCountdown = 2f;

    public HealthBar healthBar;

    private Animator animator;
    public bool attackAnimation;
    public bool hittedOnce;
    public float swordDamage = 1;

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
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        lookUp = y > 0 ? true : false;
        lookDown = y < 0 ? true : false;

        Vector3 move = new Vector3(x, 0, 0);
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (x < 0 && transform.rotation.y != -90 && !animator.GetBool("Attack"))
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        if (x > 0 && transform.rotation.y != 90 && !animator.GetBool("Attack"))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        if (x != 0 && isGrounded)
        {
            animator.SetBool("move", true);
        }
        else
        {
            animator.SetBool("move", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.K) && !animator.GetBool("Attack"))
        {
            animator.SetBool("Attack", true);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (knockUpCountdown && currentKnockUpCountdown > 0)
        {
            currentKnockUpCountdown -= 1 * Time.deltaTime;
            controller.Move(new Vector3(-0.5f, 0.5f, 0) * 12f * Time.deltaTime);
            Debug.Log(currentKnockUpCountdown);
        }
        if (currentKnockUpCountdown < 0)
        {
            currentKnockUpCountdown = startKnockUpCountdown;
            knockUpCountdown = false;
            Debug.ClearDeveloperConsole();
        }
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
        if (other.gameObject.tag == "EnemyCrab")
        {
            knockUpCountdown = true;
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
}
