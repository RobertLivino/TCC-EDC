using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject imgComandos; // 

    // Função que será chamada ao clicar no botão
    public void MostrarComandos()
    {
        
        imgComandos.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            imgComandos.SetActive(false);
        }
    }
}