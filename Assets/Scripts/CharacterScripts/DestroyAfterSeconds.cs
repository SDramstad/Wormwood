using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {
    
    public int deathTime;

	// Use this for initialization
	void Start () {
        Debug.Log("Starting death for " + deathTime);
        StartCoroutine("Death");
	}

    IEnumerator Death()
    {
        Debug.Log("DYING :3");
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
	
}
