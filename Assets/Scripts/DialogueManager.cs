using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject chatPanel;
    public Text ChatText;
    public Button nextButton;
    private string[] messages;
    private int currentMessageIndex;
    void Start()
    {
        nextButton.onClick.AddListener(DisplayNextMessage);
        chatPanel.SetActive(false);
    }

    public void StartDialogue(string[] dialogue)
    {
        messages = dialogue;
        currentMessageIndex = 0;
        chatPanel.SetActive(true);
        DisplayNextMessage();
    }

    public void DisplayNextMessage()
    {
        if (currentMessageIndex < messages.Length)
        {
            ChatText.text = messages[currentMessageIndex];
            currentMessageIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        chatPanel.SetActive(false);
    }
}
