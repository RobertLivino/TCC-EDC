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
    public Transform visaoCostas;
    public LayerMask playerLayerMask;
    private Animator animator;

    public GameObject SpellPointCast;
    public Vector3 SpellDestination;
    public GameObject spellToCast;
    public float spellSpeed;
    public float spellDamage = 2;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.FillHealthStart(4f);
        animator = GetComponent<Animator>();
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
        Ray rayFrente = new Ray(visao.transform.position, visao.transform.forward);
        Ray rayCosta = new Ray(visaoCostas.transform.position, visaoCostas.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(rayFrente, out hit, 5,playerLayerMask) || Physics.Raycast(rayCosta, out hit, 5, playerLayerMask))
        {
            if (hit.transform.position.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            if (hit.transform.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
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
    public void CastSpell()
    {
        var SpellObj = Instantiate(spellToCast, SpellPointCast.transform.position, Quaternion.identity) as GameObject;
        SpellObj.GetComponent<Rigidbody>().velocity = SpellPointCast.transform.forward.normalized * spellSpeed;
        Destroy(SpellObj, 15f);
    }
}
