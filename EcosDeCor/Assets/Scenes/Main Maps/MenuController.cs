using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject imgComandos; // arraste sua imagem para este campo no Inspector

    // Função que será chamada ao clicar no botão
    public void MostrarComandos()
    {
        // Alterna entre ativar e desativar a imagem
        imgComandos.SetActive(!imgComandos.activeSelf);
    }
}