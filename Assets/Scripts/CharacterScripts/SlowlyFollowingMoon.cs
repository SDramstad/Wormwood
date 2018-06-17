using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowlyFollowingMoon : MonoBehaviour {

    private GameObject _player;

    [SerializeField]
    private float speed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _player = GameObject.Find("Player").gameObject;
        transform.LookAt(_player.transform);

        if (!(Vector3.Distance(transform.position, _player.gameObject.transform.position) < 5f))
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
        }
	}
}
