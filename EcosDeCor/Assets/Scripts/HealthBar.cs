using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void FillHealthStart(float update)
    {
        maxHealth = update;
        currentHealth = maxHealth;
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


