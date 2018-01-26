using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
	public float speed;
	//private Done_GameController GC;
	private Done_PlayerController PC;

	void Start ()
	{
		//GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		GameObject PlayerObject = GameObject.FindGameObjectWithTag ("Player");
		//GC = gameControllerObject.GetComponent<Done_GameController> ();
		PC = PlayerObject.GetComponent<Done_PlayerController> ();
		speed = (Time.time + speed - PC.prevTime)/ 10;
		GetComponent<Rigidbody>().velocity =  new Vector3(0, 1, 0) * speed;
	}
		//gameController.AddScore(scoreValue);
		//Destroy (other.gameObject);
		//Destroy (gameObject);

}
