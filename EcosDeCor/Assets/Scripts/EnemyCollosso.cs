using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCollosso : MonoBehaviour
{
    public PlayerScript playerScript;
    public MapaController mapaController;
    public HealthBar healthBar;
    public Transform visao;
    public Transform visaoCostas;
    public LayerMask playerLayerMask;
    public NavMeshAgent agent;
    private Animator animator;
    public float MoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.FillHealthStart(10f);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar.currentHealth <= 0 && animator.GetBool("Death") == false)
        {
            mapaController.healthMana = true;
            mapaController.healthManaValue = 20f;
            mapaController.healthHealt = true;
            mapaController.healthHealtValue = 3f;
            animator.SetBool("Death", true);
        }
        Ray rayFrente = new Ray(visao.transform.position, visao.transform.forward);
        Ray rayCosta = new Ray(visaoCostas.transform.position, visaoCostas.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(rayFrente, out hit, 10, playerLayerMask) || Physics.Raycast(rayCosta, out hit, 10, playerLayerMask))
        {
            agent.SetDestination(hit.transform.position);
            agent.speed = MoveSpeed;
            if (hit.transform.position.x > transform.position.x)
            {
                if (hit.transform.position.x - transform.position.x > 5 && !animator.GetBool("Attack"))
                {
                    animator.SetBool("Move", true);
                    agent.isStopped = false;
                    animator.SetBool("Attack", false);
                }
                else
                {
                    animator.SetBool("Move", false);
                    agent.isStopped = true;
                    transform.position = transform.position;
                    animator.SetBool("Attack", true);
                }
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            if (hit.transform.position.x < transform.position.x)
            {
                if (hit.transform.position.x - transform.position.x < -5 && !animator.GetBool("Attack"))
                {
                    animator.SetBool("Move", true);
                    agent.isStopped = false;
                    animator.SetBool("Attack", false);
                }
                else
                {
                    animator.SetBool("Move", false);
                    agent.isStopped = true;
                    transform.position = transform.position;
                    animator.SetBool("Attack", true);
                }
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
        else
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Move", false);
            agent.isStopped = true;
            transform.position = transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            healthBar.TakeDamage(playerScript.swordDamage);
        }
        if (other.tag == "Spell")
        {
            healthBar.TakeDamage(playerScript.spellDamage);
        }
    }
    public void FisinhAttack()
    {
        animator.SetBool("Attack", false);
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
