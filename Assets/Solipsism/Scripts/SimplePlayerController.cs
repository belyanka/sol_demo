using CnControls;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    public float WalkingSpeed = 7;
    public float JumpTakeOffSpeed = 7;
    public float CheckGroundRadius = 0.2f;
    public Direction InitialDirection = Direction.Right;

    private GameController _gameController;

    private Transform _checkGroundCenter;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private LayerMask _groundMask;

    private Direction _direction;
    private bool _jumpNextFixedUpdate;

    // Use this for initialization
    public void Awake()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _groundMask = 1 << LayerMask.NameToLayer("Ground");
    }

    public void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _checkGroundCenter = transform.Find("CheckGroundCenter");
        _direction = InitialDirection;
    }

    private void Update()
    {
        if (CnInputManager.GetButtonDown("Jump") && IsGrounded())
        {
            _jumpNextFixedUpdate = true;
        }
    }

    public void FixedUpdate()
    {
        Vector2 currentVelocity = _rigidBody.velocity;
        currentVelocity.x = WalkingSpeed * CnInputManager.GetAxisRaw("Horizontal");

        Direction directionBeforeUpdate = _direction;
        if (currentVelocity.x > 0) _direction = Direction.Right;
        if (currentVelocity.x < 0) _direction = Direction.Left;
        if (directionBeforeUpdate != _direction) FlipSprite();

        if (_jumpNextFixedUpdate)
        {
            currentVelocity.y = JumpTakeOffSpeed;
            _jumpNextFixedUpdate = false;
        }
        else if (CnInputManager.GetButtonUp("Jump"))
        {
            if (currentVelocity.y > 0)
            {
                currentVelocity.y = currentVelocity.y * 0.5f;
            }
        }

        _rigidBody.velocity = currentVelocity;
    }

    private void FlipSprite()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_checkGroundCenter.position, CheckGroundRadius, _groundMask) != null;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            SetDead();
        }
    }

    public void SetDead()
    {
        _gameController.GameOver();
    }

    public enum Direction
    {
        Left,
        Right
    }
}