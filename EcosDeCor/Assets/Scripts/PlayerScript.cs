using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    private bool lookUp;
    private bool lookDown;
    private float moveSpeed = 12f;
    private float gravity = -19.81f;

    private Animator animator;

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
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        lookUp = y > 0 ? true : false;
        lookDown = y < 0 ? true : false;

        Vector3 move =  new Vector3(x, 0, 0);
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (x < 0 && transform.rotation.y != -90)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        if (x > 0 && transform.rotation.y != 90)
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

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
