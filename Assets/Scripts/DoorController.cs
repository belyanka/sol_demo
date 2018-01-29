using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : TimeStopable, IButtonSubscriber
{
    public List<ButtonController> buttons;
    public bool isClosedInitially = true;
    public bool isInverted;

    private Sprite _sprite;
    private int _pressedButtonsNum;


    // Use this for initialization
    private void Awake()
    {
        _pressedButtonsNum = 0;
        _sprite = GetComponentInChildren<SpriteRenderer>().sprite;

        if (!isClosedInitially) Open();
    }

    // Use this for binding after init
    void Start()
    {
        if (buttons != null) buttons.ForEach(b => b.Subscribe(this));
    }

    public void Open()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().sprite = default(Sprite);
    }

    public void Close()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponentInChildren<SpriteRenderer>().sprite = _sprite;
    }

    public void OnButtonPressed(ButtonController b)
    {
        _pressedButtonsNum++;
        if (isInverted) Close();
        else if (_pressedButtonsNum == buttons.Count)
            Open();
    }

    public void OnButtonReleased(ButtonController b)
    {
        _pressedButtonsNum--;
        if (isInverted && _pressedButtonsNum == 0) Open();
        else Close();
    }
}