using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("HUB");
    }
    public void ExitGame()

    {
            Application.Quit();
            Debug.Log("IS EXIT");
    }
}