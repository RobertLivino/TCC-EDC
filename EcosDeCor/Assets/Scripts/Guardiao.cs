using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMP;

public class Guardiao : MonoBehaviour
{
    public MapaController mapaController;
    private Animator animator;
    public GameObject toStartInteraction;
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mapaController.guardianRange && mapaController.guardianDoor && !mapaController.startDialogue)
        {
            ShowConversationInteraction();
        }
        else
        {
            HideConversationInteraction();
        }
        if (mapaController.guardianRange && mapaController.guardianDoor && mapaController.startDialogue)
        {
            StartDialogue();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            mapaController.guardianRange = true;
            animator.SetBool("StandUp", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("StandUp", false);
            mapaController.guardianRange = false;
        }
    }
    private void ShowConversationInteraction()
    {
        toStartInteraction.SetActive(true);
    }
    private void HideConversationInteraction()
    {
        toStartInteraction.SetActive(false);
    }
    private void StartDialogue()
    {
        dialogueBox.SetActive(false);
    }
}
