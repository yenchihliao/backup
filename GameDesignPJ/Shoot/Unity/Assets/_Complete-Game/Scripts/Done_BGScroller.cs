using UnityEngine;
using System.Collections;

public class Done_BGScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeZ;
	public Done_PlayerController PC;

	private Vector3 startPosition;

	void Start ()
	{
		startPosition = transform.position;
	}

	void Update ()
	{
		scrollSpeed = (Time.time + scrollSpeed - PC.prevTime) / 10;
		float newPosition = Mathf.Repeat((Time.time - PC.prevTime) * scrollSpeed, tileSizeZ);
		transform.position = startPosition + new Vector3 (0, 1, 0) * newPosition;
	}
}