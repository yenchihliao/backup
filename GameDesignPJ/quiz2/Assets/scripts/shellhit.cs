using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellhit : MonoBehaviour {
	public float m_MaxLifeTime = 2f;
	public float speed = 2f;
	private Rigidbody Rig;
	// Use this for initialization
	void Start () {
		Rig = GetComponent<Rigidbody> ();
		Destroy (gameObject, m_MaxLifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = transform.localPosition * 1 * speed * Time.deltaTime;
		Rig.MovePosition (Rig.position + movement);
	}
	void OnTriggerEnter (Collider other) {
		Destroy (gameObject);
		print ("trigger");
	}

}

	

	