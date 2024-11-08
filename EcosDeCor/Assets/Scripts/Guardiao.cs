using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardiao : MonoBehaviour
{
    public MapaController mapaController;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            mapaController.guardianRange = true;
            animator.SetBool("StandUp", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("StandUp", false);
            mapaController.guardianRange = false;
        }
    }
}
