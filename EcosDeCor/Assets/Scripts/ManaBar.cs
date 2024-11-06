using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Image manaBar;
    public float maxMana;
    public float currentMana;

    void Start()
    {
        currentMana = maxMana;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FillManaStart(float update)
    {
        maxMana = update;
        currentMana = maxMana;
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
