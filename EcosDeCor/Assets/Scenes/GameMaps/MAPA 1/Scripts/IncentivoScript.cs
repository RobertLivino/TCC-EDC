using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncentivoScript : MonoBehaviour
{
    public GameObject text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        text.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }
}
