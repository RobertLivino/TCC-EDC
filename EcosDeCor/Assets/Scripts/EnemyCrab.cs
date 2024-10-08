using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrab : MonoBehaviour
{
    public PlayerSword playerSword;
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
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (playerSword.playerScript.hittedOnce && other.tag == "Player")
        {
            healthBar.TakeDamage(playerSword.playerScript.swordDamage);
        }
    }
}
