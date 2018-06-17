using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingChatCharacter : MonoBehaviour {

    [SerializeField]
    private string _characterDialogue;    

    [SerializeField]
    private int _timeToDisplay;

    private UIManager _ui;

    [SerializeField]
    private GameObject _textObject;

    private bool _isInBounds;
    private bool _hasTalked;

    // Use this for initialization
    void Start()
    {
        _ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        _hasTalked = false;
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
		if (_isInBounds && !_hasTalked)
        {
            Debug.Log("This should be created.");
            CreateText(_characterDialogue);
            StartCoroutine("WaitToReinstantiate");
            _hasTalked = true;
        }
	}

    IEnumerator WaitToReinstantiate()
    {
        yield return new WaitForSeconds(_timeToDisplay + 1);
        _hasTalked = false;
    }

    void CreateText(string text)
    {
        Vector3 position = transform.position;
        //Debug.Log("Start Position = " + transform.position);
        position.y += 2;

        //Debug.Log("Goal Position = " + position);
        //the floating text should self destruct on it's own
        var floatingText = Instantiate(_textObject, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);

        //set the text's attributes
        floatingText.GetComponent<TextMeshPro>().text = _characterDialogue;
        floatingText.transform.position = position;
        floatingText.GetComponent<DestroyAfterSeconds>().deathTime = _timeToDisplay;
        //Debug.Log("End Position = " + transform.position);
    }
}
