using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameras : MonoBehaviour {
	public Camera cam1;
	public Camera cam2;
	// Use this for initialization
	void Start () {
		cam1.enabled = true;
		cam2.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			if (cam1.enabled == true) {
				cam1.enabled = false;
				cam2.enabled = true;
			} else {
				cam1.enabled = true;
				cam2.enabled = false;
			
			}
		}
	}
}
