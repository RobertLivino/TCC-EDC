using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class EcoScript : MonoBehaviour
{
    public GameObject noColorFilter;
    public GameObject Portal;
    public PersistenceData persistenceData;
    // Start is called before the first frame update
    void Start()
    {
        string myTag = gameObject.tag;
        if ((myTag == "EcoCastle" && persistenceData.collectedCastleEco) || (myTag == "EcoDesert" && persistenceData.collectedCastleEco))
        {
            noColorFilter.SetActive(false);
            Portal.SetActive(true);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        string otherTag = other.gameObject.tag;
        string myTag = gameObject.tag;
        if (otherTag == "Player" && myTag == "EcoCastle")
        {
            Portal.SetActive(true);
            persistenceData.collectedCastleEco = true;
            noColorFilter.SetActive(false);
            Destroy(gameObject);
        }
        if (otherTag == "Player" && myTag == "EcoDesert")
        {
            Portal.SetActive(true);
            persistenceData.collectedDesertEco = true;
            noColorFilter.SetActive(false);
            Destroy(gameObject);
        }
    }
}
