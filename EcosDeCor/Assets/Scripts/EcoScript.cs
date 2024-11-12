using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcoScript : MonoBehaviour
{
    public GameObject noColorFilter;
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
        noColorFilter.SetActive(false);
        Destroy(gameObject);
    }
}
