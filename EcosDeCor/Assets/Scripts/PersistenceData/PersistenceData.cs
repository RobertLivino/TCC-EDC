using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class PersistenceData : ScriptableObject
{
    public int ecosColected;
    public bool collectedCastleEco;
    public bool collectedDesertEco;
    public bool hasEnterCastle;
    public bool hasEnterDesert;
    public float MasterSound;
    public float SFXSound;
    public float MusicSound;
}
