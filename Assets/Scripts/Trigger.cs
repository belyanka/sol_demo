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
		OnStart();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (IsPressed && IsMustBePressed())
		{
			Release();
		}
		else if(!IsPressed && IsMustBePressed())
		{
			Press();
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		
	}
}
