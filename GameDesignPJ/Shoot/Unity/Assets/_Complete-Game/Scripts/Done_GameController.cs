using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Done_GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Done_PlayerController PC;
	
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	
	private bool gameOver;
	private bool restart;
	private int score;
	
	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}
	
	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				PC.prevTime = Time.time;
				score = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

			}
		}

	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				int num = Random.Range (0, hazards.Length);
				GameObject hazard = hazards [num];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				if (num == 3) {
					spawnRotation = Quaternion.Euler (90, 180, -180);
					Debug.Log ("here");
				}
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
			if (gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		if (gameOver)
			return;
		//Debug.Log ("update score");
		scoreText.text = "Score: " + score + ", Health: " + PC.health;
	}
	IEnumerator Wait4Re()
	{
		print(Time.time);
		yield return new WaitForSeconds(5);
		print(Time.time);
		Application.LoadLevel(0);
	}
	public void GameOver ()
	{
		gameOverText.text = "Game Over!\n" + "Ur Score is:\n" + score;
		scoreText.text = "";
		gameOver = true;

		StartCoroutine (Wait4Re ());
	}
}