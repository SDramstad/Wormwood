using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CHAR_LongConversation1 : MonoBehaviour {

    private UIManager _ui;

    private bool _isInBounds;
    private MouseLook playerLook;
    public Dialogue dialogue;

    // Use this for initialization
    void Start () {
        _ui = FindObjectOfType<UIManager>();
	}
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            _isInBounds = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
            _isInBounds = false;
        }
    }
    // Update is called once per frame
    void Update () {
		if (_isInBounds && Input.GetKeyDown(KeyCode.E))
        {
            _ui.ShowMouse();
            GameObject.Find("Player").GetComponent<RigidbodyFirstPersonController>().enabled = false;
            FindObjectOfType<DialogueManager>().StartLinearDialogue(dialogue);
        }
	}
}
