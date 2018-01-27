using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : TimeStopable, IButtonSubscriber
{
    public List<ButtonController> buttons;

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
        
        if(buttons != null)buttons.ForEach(b => b.Subscribe(this));    
        
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
        if (_pressedButtonsNum == buttons.Count) Open();
    }

    public void OnButtonReleased(ButtonController b)
    {
        _pressedButtonsNum--;
        Close();
    }
}