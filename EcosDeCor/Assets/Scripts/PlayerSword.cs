using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public PlayerScript playerScript;
    public bool enterAttack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider boxcolider = GetComponent<BoxCollider>();
        boxcolider.enabled = playerScript.hittedOnce;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (playerScript.attackAnimation && other.gameObject.tag == "EnemyCrab" || other.gameObject.tag == "EnemyNotCrab")
        {
            enterAttack = true;
        }
    }
}
