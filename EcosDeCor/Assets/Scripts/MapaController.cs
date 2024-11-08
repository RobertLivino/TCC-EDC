using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

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
    public bool guardianDoor;
    public bool startDialogue;

    //pause
    public GameObject pauseMenu;

    //Audio
    public bool walking;
    public AudioSource playerActions;
    public AudioSource playerWalk;
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
        guardianRange = false;
        startDialogue = false;
        guardianDoor = false;
    }
    void Update()
    {
        memoriesCristalColected.text = memoriesColected.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        if (pauseMenu.activeSelf)

        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
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
        playerWalk.clip = walkAudioPlayer;
        walking = true;
        playerWalk.Play();
    }
    public void StopWalkAudio()
    {
        walking = false;
        playerWalk.Stop();
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
