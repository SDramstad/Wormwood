using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MouthyWalker : MonoBehaviour {

    public Animator animations;

    public float range;
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform player;

	// Use this for initialization
	void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Red Ball").transform;
    }
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(transform.position, player.position) <= range && Vector3.Distance(transform.position, player.position) >= 5)
        {
            transform.LookAt(player);
            animations.Play("Run");
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {   
            animations.Play("Idle");
            agent.isStopped = true;
            StopAllCoroutines();
            StartCoroutine("Munch");
        }
        
    }

    IEnumerator Munch()
    {
        player.GetComponent<Rigidbody>().AddForce(transform.forward * 11f);
        yield return new WaitForSeconds(1f);
    }
}
