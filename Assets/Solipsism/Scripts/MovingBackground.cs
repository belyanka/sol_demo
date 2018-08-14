using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Repeats children objects
 */
public class MovingBackground : MonoBehaviour {

	public float scrollSpeed;
	public bool repeated;
	public float panelSize;
	public float scrollViewSize;
	
	private List<Transform> panels;
	private Vector3 lastCameraPosition;
	private Camera camera;

	private void Start()
	{
		camera = Camera.main;
		lastCameraPosition = camera.transform.position;
		panels = new List<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			panels.Add(transform.GetChild(i));
		}
	}

	// Update is called once per frame
	void Update ()
	{
		float offset = camera.transform.position.x - lastCameraPosition.x;
		transform.Translate(offset*scrollSpeed,0f,0f);

		lastCameraPosition = camera.transform.position;
		
		if (repeated)
		{
			float currentPosition = panels[1].transform.position.x;

			if ( camera.transform.position.x > currentPosition+scrollViewSize)
			{
				//move first panel
				panels[0].transform.position = new Vector3(panels[0].transform.position.x + panels.Count*panelSize,
					panels[0].transform.position.y, panels[0].transform.position.z);
				
				panels.Add(panels[0]);
				panels.RemoveAt(0);
			}
			else if (camera.transform.position.x < currentPosition-scrollViewSize)
			{
				//move third panel
				panels[panels.Count-1].transform.position = new Vector3(panels[panels.Count-1].transform.position.x - panels.Count*panelSize,
					panels[panels.Count-1].transform.position.y, panels[panels.Count-1].transform.position.z);
				
				panels.Insert(0,panels[panels.Count-1]);
				panels.RemoveAt(panels.Count-1);
			}

		}
	}
}
