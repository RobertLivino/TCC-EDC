using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DesertPortalAlertScript : MonoBehaviour
{
    public GameObject alertBox;
    public TextMeshProUGUI alertText;
    public PersistenceData persistenceData;
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
        if(!persistenceData.hasEnterCastle)
        {
            alertBox.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        alertBox.SetActive(false);
    }
}
