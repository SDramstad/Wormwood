using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class DialogueManager : MonoBehaviour {

    //FIFO collection, first in first out
    private Queue<string> sentences;

    private Dialogue currentDialogue;
    private DialogueTree _dialogueTree;

    //FOR TESTING ONLY, MUST BE MOVED TO UI MANAGER
    public Text name;
    public TextMeshProUGUI dialogueText;
    public Image faceImage;

    public GameObject option1;
    public GameObject option2;
    public GameObject option3;
    public GameObject option4;

    //Mode switch
    private bool isDialogueTree;

    //animator
    public Animator animator;

    // Use this for initialization
    void Start () {
        sentences = new Queue<string>();
	}

    public void DisplayNextSentence()
    {
        if (isDialogueTree)
        {
            DisplayNextSentenceTree();
        }
        else
        {
            DisplayNextSentenceLinearly();
        }
    }

    public void StartLinearDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        isDialogueTree = false;

        name.text = dialogue.name;

        option1.SetActive(false);
        option2.SetActive(false);
        option3.SetActive(false);
        option4.SetActive(true);
        option4.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        
        faceImage.overrideSprite = dialogue.face;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void StartDialogueTree(DialogueTree dialogueTree)
    {
        animator.SetBool("IsOpen", true);
        isDialogueTree = true;
        sentences.Clear();
        ResetChoices();
        Debug.Log("Start Dialogue Tree");
        //Gets the starter node
        _dialogueTree = dialogueTree;
        currentDialogue = Array.Find(_dialogueTree.dialogues, item => item.id == "starter");
        SetupNewDialogue();
    }

    private void SetupNewDialogue()
    {
        name.text = currentDialogue.name;
        faceImage.overrideSprite = currentDialogue.face;

        sentences.Clear();
        foreach (string sentence in currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentenceLinearly()
    {
        if (sentences.Count == 0)
        {
            //end dialogue
            EndDialogue();
        }
        else
        {
            string sentence = sentences.Dequeue();
            dialogueText.text = sentence;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        
    }

    public void DisplayNextSentenceTree()
    {
        //Debug.Log("DisplayNextSentenceTree Sentence count " + sentences.Count);
        //if (sentences.Count == 0)
        //{
        //    //display buttons
        //    DisplayChoices();

        //}
        //else
        //{
        //    string sentence = sentences.Dequeue();
        //    dialogueText.text = sentence;
        //    StopAllCoroutines();
        //    StartCoroutine(TypeSentence(sentence));

        //    //SET UP BUTTONS
        //    ResetChoices();
        //    option4.SetActive(true);
        //    option4.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        //}
        if (sentences.Count != 0)
        {
            string sentence = sentences.Dequeue();
            dialogueText.text = sentence;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        

        if (sentences.Count == 0)
        {
            DisplayChoices();
        }
        else
        {
            //SET UP BUTTONS
            ResetChoices();
            option4.SetActive(true);
            option4.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        }
        
    }

    private void DisplayChoices()
    {
        Debug.Log("Displaying choices.");
        foreach (Responses response in currentDialogue.responses)
        {
            Debug.Log(response.displayText + " which leads to " + response.targetDialogueID);
        }

        ResetChoices();

        //only supports up to 4 options right now
        if (currentDialogue.responses.Length <= 4)
        {
            //always display at least one option
            option1.SetActive(true);
            option1.GetComponentInChildren<TextMeshProUGUI>().text = currentDialogue.responses[0].displayText;
            option1.GetComponent<Button>().onClick.AddListener(() => { GoToNodeEvent(currentDialogue.responses[0].targetDialogueID); });

            if (currentDialogue.responses.Length >= 2)
            {
                option2.SetActive(true);
                option2.GetComponent<Button>().onClick.AddListener(() => { GoToNodeEvent(currentDialogue.responses[1].targetDialogueID); });
                option2.GetComponentInChildren<TextMeshProUGUI>().text = currentDialogue.responses[1].displayText;

                if (currentDialogue.responses.Length >= 3)
                {
                    option3.SetActive(true);
                    option3.GetComponentInChildren<TextMeshProUGUI>().text = currentDialogue.responses[2].displayText;
                    option3.GetComponent<Button>().onClick.AddListener(() => { GoToNodeEvent(currentDialogue.responses[2].targetDialogueID); });

                    if (currentDialogue.responses.Length >= 4)
                    {
                        option4.SetActive(true);
                        option4.GetComponentInChildren<TextMeshProUGUI>().text = currentDialogue.responses[3].displayText;
                        option4.GetComponent<Button>().onClick.AddListener(() => { GoToNodeEvent(currentDialogue.responses[3].targetDialogueID); });
                    }
                }
            }


        }

    }

    private void GoToNodeEvent(string targetDialogueID)
    {
        //If choice leaves conversation
        if (targetDialogueID == "end")
        {
            EndDialogue();
        }
        //else if choice starts a new dialogue
        else
        {
            currentDialogue = Array.Find(_dialogueTree.dialogues, item => item.id == targetDialogueID);
            SetupNewDialogue();
        }
    }

    private void ResetChoices()
    {
        //make sure all options are disabled first
        option1.SetActive(false);
        option2.SetActive(false);
        option3.SetActive(false);
        option4.SetActive(false);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        FindObjectOfType<UIManager>().HideMouse();
        GameObject.Find("Player").GetComponent<RigidbodyFirstPersonController>().enabled = true;
        animator.SetBool("IsOpen", false);
        
    }
}
