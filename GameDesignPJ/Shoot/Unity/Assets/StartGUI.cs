using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGUI : MonoBehaviour {

	public int a, b, c, d;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		if (GUI.Button (new Rect (a, b, c, d), "START/RESTART")) {
			Application.LoadLevel("_Complete-Game");
		}
	}
}
