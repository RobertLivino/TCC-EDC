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
            if (persistenceData.hasEnterCastle)
            {
                SceneManager.LoadScene("MAPA 2");
                persistenceData.hasEnterDesert = true;
            }
        }
        if (otherTag == "Player" && myTag == "PortalFinal")
        {
            if (persistenceData.hasEnterDesert && persistenceData.hasEnterCastle)
            {
                SceneManager.LoadScene("MAPA 3");
            }
        }
        if (otherTag == "Player" && myTag == "ReturnToHub")
        {
            SceneManager.LoadScene("HUB");
        }
        if (otherTag == "Player" && myTag == "PortalEndGame")
        {
            SceneManager.LoadScene("ENDGAME");
        }
    }
}
