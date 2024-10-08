using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Image manaBar;
    public float maxMana = 100f;
    private float currentMana;

    void Start()
    {
        currentMana = maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMana < 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            useMana(10);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            HealMana(10);
        }
    }
    public void useMana(float damage)
    {
        currentMana -= damage;
        manaBar.fillAmount = currentMana / maxMana;
    }

    public void HealMana(float heal)
    {
        currentMana += heal;

        manaBar.fillAmount = currentMana / maxMana;
    }
}
