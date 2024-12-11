using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubControl : MonoBehaviour
{
    public PersistenceData persistenceData;
    public GameObject door1;
    public GameObject door2;
    public GameObject doorCollider;
    void Start()
    {
        if (persistenceData.hasEnterDesert && persistenceData.hasEnterCastle)
        {
            door1.SetActive(false);
            door2.SetActive(false);
            doorCollider.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
