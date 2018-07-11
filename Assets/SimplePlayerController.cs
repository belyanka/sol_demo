using CnControls;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
	public float walkingSpeed = 7;
	public float jumpTakeOffSpeed = 7;
	public float checkGroundRadius = 0.2f;
	public GameController gameController;

	private Transform checkGroundCenter;
	private Rigidbody2D rigidBody;
	private LayerMask groundMask;


	// Use this for initialization
	public void Awake()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		groundMask = 1 << LayerMask.NameToLayer("Ground");
	}

	public void OnEnable()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		checkGroundCenter = transform.Find("CheckGroundCenter");
	}

	public void FixedUpdate()
	{
		Vector2 currentVelocity = rigidBody.velocity;
		currentVelocity.x = walkingSpeed * CnInputManager.GetAxisRaw("Horizontal");
		
		//todo: пофиксить прыжок - нажатия на кнопку обрабатываются не всегда, возможно их стоит делать в Update()
		if (CnInputManager.GetButtonDown("Jump") && IsGrounded())
		{
			currentVelocity.y = jumpTakeOffSpeed;
		}
		else if (CnInputManager.GetButtonUp("Jump"))
		{
			if (currentVelocity.y > 0)
			{
				currentVelocity.y = currentVelocity.y * 0.5f;
			}
		}

		rigidBody.velocity = currentVelocity;
	}

	private bool IsGrounded()
	{
		return Physics2D.OverlapCircle(checkGroundCenter.position, checkGroundRadius, groundMask) != null;
	}
	
	public void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("StickySurface"))
		{
			transform.parent = other.transform;
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("StickySurface"))
		{
			transform.parent = null;
		}
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
		gameController.GameOver();
	}
}