using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class Guardiao : MonoBehaviour
{
    public MapaController mapaController;
    private Animator animator;
    
    public GameObject toStartInteraction;
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed;
    private int index;
    private bool activeDialogueBox;

    void Start()
    {
        animator = GetComponent<Animator>();
        dialogueText.text = string.Empty;
        activeDialogueBox = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mapaController.guardianRange && mapaController.guardianDoor && !mapaController.startedDialogue)
        {
            ShowConversationInteraction();
        }
        else
        {
            HideConversationInteraction();
        }
        if (mapaController.startedDialogue && !activeDialogueBox)
        {
            mapaController.blockConversation = true;
            StartDialogue();
        }
        if (mapaController.startedDialogue && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
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
        dialogueBox.SetActive(true);
        activeDialogueBox = true;
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueBox.SetActive(false);
            activeDialogueBox = false;
            mapaController.startedDialogue = false;
        }
    }
}
