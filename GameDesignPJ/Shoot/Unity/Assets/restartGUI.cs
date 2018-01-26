using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class retartGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		if (GUI.Button (new Rect (100, 100, 100, 100), "RESTART")) {
			Application.LoadLevel(1);
		}
	}
}
