using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : TimeStopable {

	private Rigidbody2D rb;
	private bool inSphere;
	private bool needSave = true;
	private bool needRestore = true;
	private float savedGravity;
	private Vector2 savedVelocity;

	private void Awake() {
		TimeSphere = GameObject.FindGameObjectWithTag("TimeSphere").GetComponent<Collider2D>();
		rb = GetComponent<Rigidbody2D>();
		SaveState();
	}

	private void Start()
	{
		inSphere = IsCanMove();
	}

	void FixedUpdate() {
		/*if (!IsCanMove()) {
			if (inSphere) {
				SaveState();
				rb.gravityScale = 0;
				rb.velocity=Vector2.zero;
				rb.Sleep();
				inSphere = false;
			}
		}
		else {
			if (!inSphere) {
				RestoreState();
				rb.WakeUp();
				inSphere = true;
			}
		}*/

		if (IsCanMove()) {
			if (inSphere) {
				RestoreState ();
				rb.WakeUp ();
				//needRestore = false;
				//needSave = true;
				inSphere = false;
			}
		} else {
			if (!inSphere) {
				SaveState();
				//needSave = false;
				//needRestore = true;
				inSphere = true;
			}
			Freeze();
		}
	}

	public void SaveState() {
		savedVelocity = rb.velocity;
		savedGravity = rb.gravityScale;
	}

	public void RestoreState() {
		rb.velocity = savedVelocity;
		rb.gravityScale = savedGravity;
	}

	public void Freeze() {
		rb.gravityScale = 0;
		rb.velocity=Vector2.zero;
		rb.Sleep();
	}

	public void ResetStatus() {
		inSphere = false;
		rb.gravityScale = 1f;
		rb.velocity = Vector2.zero;
		savedGravity = 1f;
		savedVelocity=Vector2.zero;
	}
}
