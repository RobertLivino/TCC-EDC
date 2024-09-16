using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth = 5f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            HealDamage(1);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void HealDamage(float heal)
    {
        currentHealth += heal;
        //currentHealth = Mathf.Clamp(heal, 0, maxHealth);

        healthBar.fillAmount = currentHealth / maxHealth;
    }
}


