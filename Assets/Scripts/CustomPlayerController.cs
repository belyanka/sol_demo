using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

public class CustomPlayerController : CustomPhysicsObject {

	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 7;
	public GameController gameController;

	private int directionX = 1;
	private SpriteRenderer spriteRenderer;
//	private Animator animator;

	// Use this for initialization
	void Awake ()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		spriteRenderer = GetComponent<SpriteRenderer> (); 
//		animator = GetComponent<Animator> ();
	}

	protected override void ComputeVelocity()
	{
		Vector2 move = Vector2.zero;

		move.x = CnInputManager.GetAxisRaw("Horizontal");

		if (CnInputManager.GetButtonDown ("Jump") && grounded) {
			velocity.y = jumpTakeOffSpeed;
		} else if (CnInputManager.GetButtonUp ("Jump")) 
		{
			if (velocity.y > 0) {
				velocity.y = velocity.y * 0.5f;
			}
		}

		//bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
		if (directionX*move.x < 0) 
		{
			spriteRenderer.flipX = !spriteRenderer.flipX;
			directionX *= -1;
		}

//		animator.SetBool ("grounded", grounded);
//		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);

		targetVelocity = move * maxSpeed;
	}
	
	public void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			SetDead();
		}
	}

	public void SetDead() {
		gameController.GameOver();
	}

	public Vector2 GetVelocity()
	{
		return velocity;
	}
}
