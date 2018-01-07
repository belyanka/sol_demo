﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour, IButtonSubscriber
{
    public ButtonController button;

    private Sprite _sprite;
    private Collider2D _doorCollider;


    // Use this for initialization
    void Start()
    {
        button.Subscribe(this);
        _sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        _doorCollider = GameObject.Find("DoorCollider").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Open()
    {
        _doorCollider.enabled = false;
        GetComponentInChildren<SpriteRenderer>().sprite = default(Sprite);
    }

    public void Close()
    {
        _doorCollider.enabled = true;
        GetComponentInChildren<SpriteRenderer>().sprite = _sprite;
    }

    public void OnButtonPressed(ButtonController b)
    {
        Open();
    }

    public void OnButtonReleased(ButtonController b)
    {
        Close();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            //todo: lvl completion
            
            Debug.Log("LVL COMPLETE");
        }
    }
}