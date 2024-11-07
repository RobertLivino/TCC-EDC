using UnityEngine;

public class AttackSound : MonoBehaviour
{
    // Referência para o AudioSource
    private AudioSource audioSource;

    void Update()
{
    void Update()
{
    if (Input.GetKeyDown(KeyCode.J)) // Usa a tecla "J" para o ataque
    {
        // Executa o ataque
        PlayAttackSound(); // Toca o som de ataque
    }
}
}


    void Start()
    {
        // Obtém o AudioSource do objeto
        audioSource = GetComponent<AudioSource>();
    }

    // Método para tocar o som
    public void PlayAttackSound()
    {
        audioSource.Play();
    }
}