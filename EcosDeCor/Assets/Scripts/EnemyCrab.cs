using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCrab : MonoBehaviour
{
    public PlayerScript playerScript;
    public MapaController mapaController;
    public HealthBar healthBar;
    public Transform visao;
    public NavMeshAgent agent;
    public LayerMask playerLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.FillHealthStart(4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar.currentHealth <= 0)
        {
            mapaController.healthMana = true;
            mapaController.healthManaValue = 10f;
            mapaController.healthHealt = true;
            mapaController.healthHealtValue = 1f;
            Destroy(gameObject);
        }
        //Debug.DrawRay(visao.transform.position, visao.transform.forward);
        Ray ray = new Ray(visao.transform.position, visao.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction);
        if (Physics.Raycast(ray, 0, playerLayerMask))
        {
            Debug.Log("Player");
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
}
