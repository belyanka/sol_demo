using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillBackground : MonoBehaviour
{

	private float oldZ;
	// Update is called once per frame
	void Update ()
	{
		oldZ = transform.position.z;
		transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y,oldZ);
	}
}
