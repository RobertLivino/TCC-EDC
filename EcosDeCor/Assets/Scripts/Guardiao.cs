using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardiao : MonoBehaviour
{
    public MapaController mapaController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            
        }
    }
}
