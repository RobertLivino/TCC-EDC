using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void ExitGame()
    {
        Application.Quit();
        Time.timeScale = 1;
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("MenuTeste");
        Time.timeScale = 1;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}