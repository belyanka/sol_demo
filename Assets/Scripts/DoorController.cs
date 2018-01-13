using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : TimeStopable, IButtonSubscriber
{
    public ButtonController button;

    private Sprite _sprite;
    [SerializeField]
    private Collider2D _doorCollider;
    [SerializeField]
    private GameController gameController;


    // Use this for initialization
    void Start()
    {
        button.Subscribe(this);
        _sprite = GetComponentInChildren<SpriteRenderer>().sprite;
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
            gameController.LevelFinished();
            
            Debug.Log("LVL COMPLETE");
        }
    }
}