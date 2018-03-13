using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ButtonController : TimeStopable, INotifier<IButtonSubscriber>
{	
	private Sprite _spriteReleased;
	public Sprite _spritePressed;
	
	protected SpriteRenderer _spriteRenderer;
	protected List<IButtonSubscriber> _subscribers;

	public bool IsPressed { get; protected set; }

	private void Awake()
	{
		OnAwake();
	}

	protected void OnAwake()
	{
		_subscribers = new List<IButtonSubscriber>();
		IsPressed = false;
	}

	private void Start()
	{
		_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		_spriteReleased = _spriteRenderer.sprite;
		//todo: избавиться от хардкода
		//_spritePressed = Resources.Load<Sprite>("Sprites/button_pressed");
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!IsPressed && IsMustBePressed()) PressAction();
	}

	private void OnTriggerExit2D(Collider2D other)
	{		
		if (IsPressed && !IsMustBePressed()) ReleaseAction();
	}

	protected bool IsMustBePressed()
	{
		return GetComponent<Collider2D>().IsTouchingLayers();
	}

	public void Press()
	{
		IsPressed = true;
		_subscribers.ForEach(subscriber => subscriber.OnButtonPressed(this));
	}

	private void PressAction() {
		GetComponentInChildren<SpriteRenderer>().sprite = _spritePressed;
		Press();
	}

	public void Release()
	{
		IsPressed = false;
		_subscribers.ForEach(subscriber => subscriber.OnButtonReleased(this));
	}

	private void ReleaseAction() {
		GetComponentInChildren<SpriteRenderer>().sprite = _spriteReleased;
		Release();
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
