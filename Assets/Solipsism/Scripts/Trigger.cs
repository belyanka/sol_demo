using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : ButtonController {
	
	private void Awake()
	{
		OnAwake();
	}

	private void Start()
	{
		_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (IsPressed)
		{
			ReleaseAction();
		}
		else
		{
			PressAction();
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		
	}

	private void PressAction() {
		_spriteRenderer.flipX=true;
		Press();
	}

	public void ReleaseAction() {
		_spriteRenderer.flipX=false;
		Release();
	}
}
