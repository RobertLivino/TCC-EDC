using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject imgComandos;
    public GameObject configAudio;
    public GameObject credits;
    public void MostrarComandos()
    {
        
        imgComandos.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            imgComandos.SetActive(false);
            credits.SetActive(false);
            configAudio.SetActive(false);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("INTRO");
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("IS EXIT");
    }
    public void ConfigSoundGame()
    {
        configAudio.SetActive(true);
    }
    public void Credits()
    {
        credits.SetActive(true);
    }
}