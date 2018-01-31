using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : TimeStopable,IButtonSubscriber {

	public List<ButtonController> buttons;
	public float initialZRotation;
	public float activeZRotation;
	public Vector3 initialPosition;
	public Vector3 activePosition;

	private bool active;

	
	// Use this for initialization
	void Start () {
		active = false;
		if(buttons != null)buttons.ForEach(b => b.Subscribe(this));   
	}

	public void OnButtonPressed(ButtonController b) {
		active = true;
		StartCoroutine("Activate");
	}

	public void OnButtonReleased(ButtonController b) {
		active = false;
		StartCoroutine("Disactivate");
	}

	IEnumerator Activate() {
		bool activated = false;
		while (!activated && active) {
			if (IsCanMove()) {
				transform.rotation = Quaternion.Euler(0,0,activeZRotation);
				transform.position = activePosition;
				activated = true;
			}
			yield return null;
		}
	}

	IEnumerator Disactivate() {
		
		bool disActivated = false;
		while (!disActivated && !active) {
			if (IsCanMove()) {
				transform.rotation = Quaternion.Euler(0,0,initialZRotation);
				transform.position = initialPosition;
				disActivated = true;
			}
			yield return null;
		}
	}
}
