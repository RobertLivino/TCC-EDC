using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrab : MonoBehaviour
{
    public PlayerScript playerScript;
    public MapaController mapaController;
    public HealthBar healthBar;
    public bool fill;
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            healthBar.TakeDamage(playerScript.swordDamage);
        }
        if (other.tag == "Spell")
        {
            healthBar.TakeDamage(playerScript.spellDamage);
        }
    }
}
