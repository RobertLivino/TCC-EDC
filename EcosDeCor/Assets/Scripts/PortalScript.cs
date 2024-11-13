using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public PersistenceData persistenceData;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        string otherTag = other.gameObject.tag;
        string myTag = gameObject.tag;
        if (otherTag == "Player" && myTag == "PortalCastle")
        {
            if (!persistenceData.hasEnterCastle)
            {
                persistenceData.hasEnterCastle = true;
            }
            SceneManager.LoadScene("MAPA 1");
        }
        if (otherTag == "Player" && myTag == "PortalDesert")
        {
            if (!persistenceData.hasEnterDesert)
            {
                persistenceData.hasEnterDesert = true;
            }
            SceneManager.LoadScene("MAPA 2");
        }
        if (otherTag == "Player" && myTag == "PortalFinal")
        {
            if (!persistenceData.hasEnterDesert && !persistenceData.hasEnterCastle)
            {
            }
        }
        if (otherTag == "Player" && myTag == "ReturnToHub")
        {
            SceneManager.LoadScene("HUB");
        }
    }
}
