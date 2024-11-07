using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapaController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool healthMana;
    public float healthManaValue;
    public bool healthHealt;
    public float healthHealtValue;
    public float memoriesColected;
    public TextMeshProUGUI memoriesCristalColected;
    public bool guardianRange;
    public AudioSource playerActions;
    public AudioSource crabActions;
    public AudioSource collossoActions;
    public AudioSource bossActions;
    public AudioClip colectedAudioPlayer;
    public AudioClip attackAudioPlayer;
    public AudioClip spellAudioPlayer;
    public AudioClip walkAudioPlayer;
    public AudioClip jumpUpAudioPlayer;
    public AudioClip jumpDownAudioPlayer;

    void Start()
    {
        healthMana = false;
        memoriesColected = 0;
    }
    void Update()
    {
        memoriesCristalColected.text = memoriesColected.ToString();
    }
    public void PlayColectedMemory()
    {
        playerActions.clip = colectedAudioPlayer;
        playerActions.Play();
    }
    public void PlayAttackAudio()
    {
        playerActions.clip = attackAudioPlayer;
        playerActions.Play();
    }
    public void PlaySpellAudio()
    {
        playerActions.clip = spellAudioPlayer;
        playerActions.Play();
    }
    public void PlayWalkAudio()
    {
        playerActions.clip = walkAudioPlayer;
        playerActions.Play();
    }
    public void PlayJumpUpAudio()
    {
        playerActions.clip = jumpUpAudioPlayer;
        playerActions.Play();
    }
    public void PlayJumpDownAudio()
    {
        playerActions.clip = jumpDownAudioPlayer;
        playerActions.Play();
    }
}
