using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : TimeStopable {

	private Rigidbody2D rb;
	private bool inSphere;
	private float savedGravity;
	private Vector2 savedVelocity;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		inSphere = false;
	}

	void FixedUpdate() {
		if (!IsCanMove()) {
			if (!inSphere) {
				savedVelocity = rb.velocity;
				savedGravity = rb.gravityScale;
				rb.gravityScale = 0;
				rb.Sleep();
				inSphere = true;
			}
		}
		else {
			if (inSphere) {
				rb.velocity = savedVelocity;
				rb.gravityScale = savedGravity;
				rb.WakeUp();
				inSphere = false;
			}
		}
	}
}
