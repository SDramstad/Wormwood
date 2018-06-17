using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkToPlayer : MonoBehaviour {

    private GameObject _player;

    private GameObject _gameManager;

	// Use this for initialization
	void Start () {
        _gameManager = GameObject.Find("StartManager");
	}
	
	// Update is called once per frame
	void Update () {
        _player = GameObject.Find("Player").gameObject;
        transform.LookAt(_player.transform);

        if (!(Vector3.Distance(transform.position, _player.gameObject.transform.position) < 5f))
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            GetComponent<NavMeshAgent>().destination = _player.transform.position;
        }
        else
        {
            _gameManager.GetComponent<StartUp>().DisablePanel();
        }
	}
}
