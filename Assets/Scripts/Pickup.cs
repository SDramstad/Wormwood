using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    
    [SerializeField]
    private string _pickupText;

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
            _ui.SetPickUpText(_pickupText);
            Destroy(gameObject);
            //if (Input.GetKeyDown(KeyCode.E))
            //{


            //}
        }
    }
}
