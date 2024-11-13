using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
        if (otherTag == "Player" && myTag == "PortalDesert")
        {
            if (!persistenceData.hasEnterDesert)
            {
                persistenceData.hasEnterDesert = true;
            }
        }
    }
}
