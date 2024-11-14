using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public PersistenceData persistenceData;
    public AudioMixer audioMixer;
    public GameObject audioConfig;
    
    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("Master", level);
    }
    public void SetSFXVolume(float level)
    {
        audioMixer.SetFloat("SFX", level);
    }
    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("Music", level);
    }
    public void Voltar()
    {
        audioConfig.SetActive(false);
    }
}
