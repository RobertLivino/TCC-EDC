using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PersistenceData : ScriptableObject
{
    public int ecosColected;
    public bool collectedCastleEco;
    public bool collectedDesertEco;
    public bool hasEnterCastle;
    public bool hasEnterDesert;
}
