using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public float Speed = 12f;
	public float TurnSpeed = 180f;

	private Rigidbody Rig;
	private float MovementInput;
	private float TurnInput;

	// Use this for initialization
	void Awake(){
		Rig = GetComponent<Rigidbody> ();		
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		MovementInput = Input.GetAxis ("Vertical");
		TurnInput = Input.GetAxis ("Horizontal");
	}
	void FixedUpdate(){
		Move2 ();
		Turn ();
	}
	void OnGUI(){
		if (GUI.Button (new Rect (10, 10, 30, 30), "left")) {
			TurnInput = 1;
			Turn ();
		}
		if (GUI.Button (new Rect (10, 100, 30, 30), "right")) {
			TurnInput = -1;
			Turn ();
		}
		if (GUI.Button (new Rect (100, 10, 30, 30), "for")) {
			MovementInput = 1;
			Move2 ();
		}
		if (GUI.Button (new Rect (100, 100, 30, 30), "back")) {
			MovementInput = -1;

			Move2 ();
		}
	}

	private void Move2(){
		Vector3 movement = transform.forward * MovementInput * Speed * Time.deltaTime;
		Rig.MovePosition (Rig.position + movement);
	}
	private void Turn(){
		float turn = TurnInput * TurnSpeed * Time.deltaTime;
		Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
		Rig.MoveRotation(Rig.rotation * turnRotation);
	}
}
