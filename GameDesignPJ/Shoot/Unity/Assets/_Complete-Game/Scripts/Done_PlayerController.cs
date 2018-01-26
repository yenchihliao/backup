using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed = 8f;
	public float tilt;
	public float down;
	public Done_GameController GC;
	public float prevTime;
	//public Done_Boundary boundary;

	private Rigidbody RG;
	public int health = 100;


	void Start(){
		prevTime = Time.time;
	
	}
	void Update ()
	{
		
	}
	void FixedUpdate ()
	{
		float down2 = down - (Time.time - prevTime) / 500; 
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector3 movement = new Vector3 (moveHorizontal, down2, 0f);
		RG = GetComponent<Rigidbody>();
		RG.velocity = movement * speed;
		//Debug.Log (RG.velocity);
		//GetComponent<Rigidbody>().position = new Vector3
		//(
		//	Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
		//	0.0f, 
		//	Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		//);
		
		//GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
	public void Demage(int a){
		health = health - a;
		if (health == 0) {
			health = 100;
			GC.GameOver ();
		}
	}
	public void UpDown(int b){
		transform.position = new Vector3 (transform.position.x, 
			transform.position.y - b, 
			transform.position.z);
	}
}
