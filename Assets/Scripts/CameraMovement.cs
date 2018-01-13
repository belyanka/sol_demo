using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Collider2D visibleArea;
	public float smoothness;
	public bool followPlayer;
	
	private GameObject player;
	private Vector3 newPosition;
	private float xMax;
	private float xMin;
	private float yMax;
	private float yMin;

	private void Start()
	{
		followPlayer = true;
		player = GameObject.FindWithTag("Player");
		xMax = visibleArea.bounds.max.x - Camera.main.orthographicSize * Screen.width / Screen.height;
		xMin = visibleArea.bounds.min.x + Camera.main.orthographicSize * Screen.width / Screen.height;
		yMax = visibleArea.bounds.max.y - Camera.main.orthographicSize;
		yMin = visibleArea.bounds.min.y + Camera.main.orthographicSize;
	}

	// Update is called once per frame
	void Update ()
	{
		if (followPlayer)
		{
			newPosition.x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
			newPosition.y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
			newPosition = Vector3.Lerp(transform.position, newPosition, smoothness * Time.deltaTime);
			transform.position = new Vector3(newPosition.x,newPosition.y,transform.position.z);
	
		}
	}
}
