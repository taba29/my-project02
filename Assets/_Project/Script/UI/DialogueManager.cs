using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    public bool IsOpen { get; private set; }

    void Awake()
    {
        CloseDialogue();
    }

    public void ShowDialogue(string message)
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        if (dialogueText != null)
            dialogueText.text = message;

        IsOpen = true;
    }

    public void CloseDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (dialogueText != null)
            dialogueText.text = "";

        IsOpen = false;
    }
}