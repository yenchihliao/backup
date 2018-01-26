using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ground : MonoBehaviour {
	public Done_PlayerController p;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter (Collider other)
	{
		Debug.Log (other.tag);
		if (other.tag == "Player") {
			p.grounded = 1;
		}
	}

}
