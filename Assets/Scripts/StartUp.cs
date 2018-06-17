using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour {

    private GameObject uiPanel;

	// Use this for initialization
	void Start () {
        uiPanel = GameObject.Find("DeathPanel");
        uiPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisablePanel()
    {
        uiPanel.SetActive(true);
    }
}
