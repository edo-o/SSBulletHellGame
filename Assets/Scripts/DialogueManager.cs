using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public GameObject chatPanel;
    public TMP_Text ChatText;
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
        if (ChatText == null)
        {
            Debug.LogWarning("ChatText is null");
            return;
        }

        if (messages == null)
        {
            Debug.LogWarning("Messages is null");
            return;
        }
        
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
