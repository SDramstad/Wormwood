using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingCharacter : MonoBehaviour {

    [SerializeField]
    private Sprite _characterImage;

    [SerializeField]
    private string _characterName;

    [SerializeField]
    private string _characterDialogue;

    private UIManager _ui;

    private bool _isInBounds;

    // Use this for initialization
    void Start () {
        _ui = GameObject.Find("UIManager").GetComponent<UIManager>();
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

            _isInBounds = false;
        }
    }

    // Update is called once per frame
    void Update () {

        if (_isInBounds)
        {
            //otherwise we can take input
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("Conversation starts here.");
                _ui.SetConversationText(_characterName, _characterDialogue, _characterImage);
                // _ui.SetConversationText(characterName, characterDialogue, durationDialogue);

            }
        }
    }
}
