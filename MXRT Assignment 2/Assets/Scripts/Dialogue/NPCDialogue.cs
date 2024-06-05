using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    [System.Serializable]
    public struct DialogueEntry
    {
        public string npcName;
        [TextArea(3, 10)]
        public string[] dialogueText;
    }

    public DialogueEntry[] initialEntries;
    public DialogueEntry[] crystalEntries;
    private DialogueEntry[] currentEntries; 

    private int currentIndex = 0;
    private int currentLineIndex = 0; 

    private bool dialogueActive = false;
    private bool crystalDialogueShown = false;

    public GameObject dialogueCanvas;
    public Text npcName;
    public Text dialogueText;

    public NavigationArrow arrowScript;

    void Start()
    {
        SetEntries(initialEntries);
    }

    private void DisplayCurrentDialogue()
    {
        if (dialogueActive && currentIndex < currentEntries.Length)
        {
            DialogueEntry currentEntry = currentEntries[currentIndex];

            //Updates the dialogue UI elements
            npcName.text = currentEntry.npcName;
            dialogueText.text = currentEntry.dialogueText[currentLineIndex];
        }
        else
        {
            //Disables UI after completing dialogue entries
            DisableUI();
        }
    }

    public void NextButtonClicked()
    {
        currentLineIndex++;

        if (currentLineIndex >= currentEntries[currentIndex].dialogueText.Length)
        {
            //Moves to next dialogue entry if current entry is done
            currentIndex++;
            currentLineIndex = 0;

            //Checks if dialogue entries are done
            if (currentIndex >= currentEntries.Length)
            { 
                //Same logic as function above
                DisableUI();
                return;
            }
        }

        DisplayCurrentDialogue();
    }

    private void DisableUI()
    {
        dialogueActive = false;
        dialogueCanvas.SetActive(false);
    }

    public void EnableDialogue()
    {
        dialogueActive = true;
        dialogueCanvas.SetActive(true);
        DisplayCurrentDialogue();
    }

    public void SetEntries(DialogueEntry[] entries)
    {
        //Sets new dialogue entries
        currentEntries = entries;

        //Resets values for next dialogue entry
        currentIndex = 0;
        currentLineIndex = 0; 
        EnableDialogue();
    }

    void Update()
    {
        //Checks if all crystals are collected
        //Switches to crystal dialogue entries 
        //Condition ensures it is only called once because it is in Update function
        if (arrowScript.GetCrystalCount() == 3 && !dialogueActive && !crystalDialogueShown)
        {
            SetEntries(crystalEntries);
            crystalDialogueShown = true; 
        }
    }
}