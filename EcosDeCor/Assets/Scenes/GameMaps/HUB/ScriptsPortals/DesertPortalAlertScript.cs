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
        if (persistenceData.hasEnterCastle && persistenceData.hasEnterDesert)
        {
            alertText.text = "O último guardião o espera";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!persistenceData.hasEnterCastle)
        {
            alertBox.SetActive(true);
        }
        if(persistenceData.hasEnterCastle && persistenceData.hasEnterDesert)
        {
            alertBox.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        alertBox.SetActive(false);
    }
}
