using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
	public float speed;
	public float bound;
	//public float force;
	private Vector3 velocity = Vector3.zero;
	void Start ()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
	void Update()
	{
		if (transform.position.z <= bound) {
			
			GetComponent<Rigidbody> ().AddForce (transform.forward * 10000);

		}
	}
}
