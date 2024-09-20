using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public DialogManager dialogueManager;
    public string[] messages;
    private bool playerInRange;

    void Start()
    {
        if (messages == null || messages.Length == 0)
        {
            Debug.LogWarning("Messages is null or empty");
        }
        else
        {
            Debug.Log("Messages array initialized with " + messages.Length + " messages.");
        }

        if (dialogueManager == null)
        {
            Debug.LogError("DialogManager is not assigned.");
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueManager != null && messages != null && messages.Length > 0)
            {
                dialogueManager.StartDialogue(messages);
                Debug.Log("Starting dialogue with messages.");
            }
            else
            {
                Debug.LogWarning("Cannot start dialogue. Either dialogueManager is null or messages are not set.");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered range.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (dialogueManager != null)
            {
                dialogueManager.EndDialogue();
                Debug.Log("Ending dialogue.");
            }
        }
    }
}