using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : TimeStopable, IButtonSubscriber
{
    public List<ButtonController> buttons;
    public Collider2D _doorCollider;

    private Sprite _sprite;
    private int _pressedButtonsNum;

    private void Awake()
    {
        _pressedButtonsNum = 0;
        _sprite = GetComponentInChildren<SpriteRenderer>().sprite;
    }

    // Use this for initialization
    void Start()
    {
        buttons.ForEach(b => b.Subscribe(this));
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
        _pressedButtonsNum++;
        if (_pressedButtonsNum == buttons.Count) Open();
    }

    public void OnButtonReleased(ButtonController b)
    {
        _pressedButtonsNum--;
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