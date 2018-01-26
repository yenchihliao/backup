using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour {
	public Transform firefrom;
	public Rigidbody shell;
	public float FireOffset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			Vector3 FireOffset2 = new Vector3(0, 1.65f, 0);
			Vector3 firebegin = firefrom.forward * FireOffset + FireOffset2;
			Rigidbody newshell = Instantiate (shell, firefrom.position + firebegin, Quaternion.Euler(firefrom.forward)) as Rigidbody;
			print ("pause");
		}
	}

}