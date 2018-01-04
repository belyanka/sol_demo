using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
	public float speed;
	
	private Vector2 direction;
	private bool grounded;
	private Rigidbody2D _rigidbody2D;

	private void Start()
	{
		direction = Vector2.right; //right
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		int test = LayerMask.NameToLayer("Ground");
		int test2 = 1 << LayerMask.NameToLayer("Ground");
		grounded = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - 3f), 1 << LayerMask.NameToLayer("Ground"));  

		if (Input.GetButtonDown("Jump") && grounded)
		{
			_rigidbody2D.AddForce(transform.up*10f, ForceMode2D.Impulse);
		}
	}
	
	private void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");

		_rigidbody2D.velocity = new Vector2(moveHorizontal * speed, _rigidbody2D.velocity.y);

	}
}
