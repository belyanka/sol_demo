﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ButtonController : MonoBehaviour, INotifier<IButtonSubscriber>
{	
	private Sprite _sprite;
	private List<IButtonSubscriber> _subscribers;

	public bool IsPressed { get; private set; }

	private void Start()
	{
		IsPressed = false;
		_subscribers = new List<IButtonSubscriber>();
		_sprite = GetComponentInChildren<SpriteRenderer>().sprite;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!IsPressed && IsMustBePressed()) Press();
	}

	private void OnTriggerExit2D(Collider2D other)
	{		
		if (IsPressed && !IsMustBePressed()) Release();
	}

	private bool IsMustBePressed()
	{
		return GetComponent<Collider2D>().IsTouchingLayers();
	}

	public void Press()
	{
		IsPressed = true;
		GetComponentInChildren<SpriteRenderer>().sprite = default(Sprite);
		_subscribers.ForEach(subscriber => subscriber.OnButtonPressed(this));
	}

	public void Release()
	{
		IsPressed = false;
		GetComponentInChildren<SpriteRenderer>().sprite = _sprite;
		_subscribers.ForEach(subscriber => subscriber.OnButtonReleased(this));
	}

	public void Subscribe(IButtonSubscriber subscriber)
	{
		_subscribers.Add(subscriber);
	}

	public void Unsubscribe(IButtonSubscriber subscriber)
	{
		_subscribers.Remove(subscriber);
	}
}