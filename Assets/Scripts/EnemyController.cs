using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public float speed;
	public Collider2D timeSphere;

	private bool isCanMove;
	private Rigidbody2D _rigidbody2D;
	private Collider2D _collider2D;

	
	// Use this for initialization
	void Start ()
	{
		isCanMove = false;
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_collider2D = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{

		isCanMove = _collider2D.bounds.Intersects(timeSphere.bounds);
		
		
		if (isCanMove) _rigidbody2D.velocity = new Vector2(-1 * speed, _rigidbody2D.velocity.y);
	}
}
