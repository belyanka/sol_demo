using System.Collections.Generic;
using UnityEngine;

public class DoorController : TimeStopable, IButtonSubscriber
{
    public List<ButtonController> buttons;
    public bool isClosedInitially = true;
    public bool isInverted;

    private Sprite _sprite;
    private int _pressedButtonsNum;
    private SpriteRenderer _spriteRenderer;


    // Use this for initialization
    private void Awake()
    {
        _pressedButtonsNum = 0;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (_spriteRenderer) _sprite = _spriteRenderer.sprite;

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
        if (_spriteRenderer) _spriteRenderer.sprite = default(Sprite);
    }

    public void Close()
    {
        GetComponent<Collider2D>().enabled = true;
        if (_spriteRenderer) _spriteRenderer.sprite = _sprite;
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