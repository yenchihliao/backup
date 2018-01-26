using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public float Speed = 80f;
	public float TurnSpeed = 300f;

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
