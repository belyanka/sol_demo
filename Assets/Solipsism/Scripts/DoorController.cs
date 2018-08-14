using System.Collections.Generic;
using UnityEngine;

public class DoorController : TimeStopable, IButtonSubscriber
{
    public List<ButtonController> Buttons;
    public ButtonsState ButtonsStateToOpen = ButtonsState.PressedAll;

    private int _pressedButtonsNum;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    // Use this for initialization
    private void Awake()
    {
        _pressedButtonsNum = 0;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        OnPressedButtonsCountChanged();
    }

    // Use this for binding after init
    void Start()
    {
        if (Buttons != null) Buttons.ForEach(b => b.Subscribe(this));
    }

    public void Open()
    {
        if (_collider) 
            _collider.enabled = false;
        if (_spriteRenderer) 
            _spriteRenderer.enabled = false;
    }

    public void Close()
    {
        if (_collider) 
            _collider.enabled = true;
        if (_spriteRenderer) 
            _spriteRenderer.enabled = true;
    }

    public void OnButtonPressed(ButtonController b)
    {
        _pressedButtonsNum++;
        OnPressedButtonsCountChanged();
    }

    public void OnButtonReleased(ButtonController b)
    {
        _pressedButtonsNum--;
        OnPressedButtonsCountChanged();
    }

    private void OnPressedButtonsCountChanged()
    {
        switch (ButtonsStateToOpen)
        {
            case ButtonsState.PressedAll:
                if (_pressedButtonsNum == Buttons.Count)
                    Open();
                else
                    Close();
                break;
            case ButtonsState.PressedAtLeastOne:
                if (_pressedButtonsNum > 0)
                    Open();
                else
                    Close();
                break;
            case ButtonsState.ReleasedAll:
                if (_pressedButtonsNum == 0)
                    Open();
                else
                    Close();
                break;
            case ButtonsState.ReleasedAtLeastOne:
                if (_pressedButtonsNum < Buttons.Count)
                    Open();
                else
                    Close();
                break;
        }
    }

    public enum ButtonsState
    {
        PressedAll,
        PressedAtLeastOne,
        ReleasedAll,
        ReleasedAtLeastOne
    }
}