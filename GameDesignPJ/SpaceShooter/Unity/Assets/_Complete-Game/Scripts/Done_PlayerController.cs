using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float gravity;
	public float height;
	 
	private Vector3 velocity = Vector3.zero;

	private float nextFire;
	private Rigidbody rb;
	public int grounded;
	private Done_GameController gameController;

	void Start()
	{
		
		rb = GetComponent<Rigidbody> ();
		grounded = 0;
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}

	}
	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			if (grounded == 1) {
				rb.AddForce (transform.forward * height);
				grounded = 0;
			}
		}
	}

	void FixedUpdate ()
	{
		if (transform.position.z <= -5) {
			gameController.GameOver ();
			Destroy (gameObject);
		
		}

		rb.AddForce (-1 * gravity * transform.forward);
		float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertical = Input.GetAxis ("Vertical");


		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 0f);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
	void OnTriggerEnter (Collider other)
	{
		Debug.Log (other.tag);
		if (other.tag == "Block") {
			grounded = 1;
		}

	}
}
