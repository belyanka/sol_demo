using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float jumpForce = 20f;
	public LayerMask groundMask;
	public Transform groundCheck; 
	public GameController gameController;
	
	private Vector2 direction;
	private bool grounded;
	private Rigidbody2D _rigidbody2D;
	private Collider2D _collider2D;

	private void Start()
	{
		direction = Vector2.right; //right
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_collider2D = GetComponent<Collider2D>();
	}

	private void Update()
	{ 
		
		//grounded = Physics2D.Linecast(transform.position, _collider2D.bounds.min + new Vector3(0, -0.01f, 0), 1 << LayerMask.NameToLayer("Ground"));
		grounded = Physics2D.OverlapCircle(groundCheck.position,0.2f,groundMask);

		//if (Input.GetButtonDown("Jump") && grounded)
		if(CnInputManager.GetButtonDown("Jump") && grounded)
		{
			_rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
		}
	}
	
	private void FixedUpdate()
	{
		//float moveHorizontal = Input.GetAxisRaw("Horizontal");
		float moveHorizontal = CnInputManager.GetAxisRaw("Horizontal");

		_rigidbody2D.velocity = new Vector2(moveHorizontal * speed, _rigidbody2D.velocity.y);

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
}
