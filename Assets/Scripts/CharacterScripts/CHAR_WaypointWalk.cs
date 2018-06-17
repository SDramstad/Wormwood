using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CHAR_WaypointWalk : MonoBehaviour {

    [SerializeField]
    private GameObject[] _walkWaypoints;

    private int currentWaypoint;
    //private GameObject _gameManager;

    // Use this for initialization
    void Start()
    {
        currentWaypoint = Random.Range(0, _walkWaypoints.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _walkWaypoints[currentWaypoint].transform.position) > 1f)
        {
            GetComponent<NavMeshAgent>().destination = _walkWaypoints[currentWaypoint].transform.position;
        }
        else
        {
            currentWaypoint = Random.Range(0, _walkWaypoints.Length);
        }
    }
}
