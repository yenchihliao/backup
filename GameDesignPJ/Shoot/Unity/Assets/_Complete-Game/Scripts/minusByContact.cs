using UnityEngine;
using System.Collections;

public class minusByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private Done_GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (tag == "Enemy2") {
			Debug.Log (other.tag);
		}
		if (other.tag != "Player")
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.tag == "Player")
		{
			Debug.Log("collide player");
			if (tag == "Boundary") {
				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
				other.gameObject.GetComponent<Done_PlayerController> ().Demage (10);
				other.gameObject.GetComponent<Done_PlayerController> ().UpDown (5);
			} else if (tag == "Enemy2") {
				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
				other.gameObject.GetComponent<Done_PlayerController> ().Demage (10);
				other.gameObject.GetComponent<Done_PlayerController> ().UpDown (-5);
				Destroy (gameObject, 0.1f);
			} else if (tag == "Enemy") {
			}
		}

		gameController.AddScore(scoreValue);
		//Destroy (other.gameObject);
	}
}