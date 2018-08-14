using System.Collections.Generic;
using UnityEngine;

public class CustomPhysicsObject : MonoBehaviour {

	
	public float gravityModifier = 1f;
	public float minGroundNormalY = .65f;

	private Rigidbody2D rigidBody;

	public Vector2 VelocityCurrent { get; private set; }

	public bool Grounded { get; private set; }

	private ContactFilter2D contactFilter;
	private readonly RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	
	private const float minMoveDistance = 0.001f;
	//todo: разобраться, зачем это было введено; связано ли это с "мягкой посадкой"?
	//это когда объект падает, за несколько пикселей до земли останавливается и медленно, мягко доходит до земли 
	//private const float shellRadius = 0.01f;

	private void OnEnable()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	void Start () 
	{
		contactFilter.useTriggers = false;
		contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
		contactFilter.useLayerMask = true;
	}
	
	private void FixedUpdate()
	{
		Vector2 velocityExpected = GetExpectedVelocity();

		Vector2 velocityActual = GetActualVelocity(velocityExpected);
		
		Vector2 deltaPosition = velocityActual * Time.deltaTime;
		VelocityCurrent = velocityActual;
		
		rigidBody.position = rigidBody.position + deltaPosition;		
	}

	/**
	 * Возвращает желаемую скорость без учета препятствий.
	 * Для рассчета может использовать текущую скорость, пользовательский ввод и пр.
	 * Базовая реализация учитывает текущую скорость и гравитацию.
	 */
	protected virtual Vector2 GetExpectedVelocity()
	{
		Vector2 velocityExpected = VelocityCurrent;
		velocityExpected.y += gravityModifier * Physics2D.gravity.y * Time.deltaTime;
		
		return velocityExpected;
	}

	protected Vector2 GetActualVelocity(Vector2 expectedVelocity)
	{
		//длина вектора перемещения, чтобы не проверять коллизии постоянно
		//UPD: пока будем проверять коллизии всегда
//		float distance = deltaPositionExpected.magnitude;
//
//		if (distance < minMoveDistance)
//		{
//			return;
//		}
		
		
		Vector2 deltaPositionExpected = expectedVelocity * Time.deltaTime;
		
		Vector2 dpHorizontal = new Vector2(deltaPositionExpected.x, 0);
		Vector2 dvHorizontal = new Vector2(expectedVelocity.x, 0);
		Vector2 actualVelocityHorizontal = GetCastCorrectedVelocity(dpHorizontal, dvHorizontal);

		
		Vector2 dpVertical = new Vector2(0, deltaPositionExpected.y);
		Vector2 dvVertical = new Vector2(0, expectedVelocity.y);
		Vector2 actualVelocityVertical = GetCastCorrectedVelocity(dpVertical, dvVertical, true);

		Vector2 velocityActual = actualVelocityHorizontal + actualVelocityVertical;

		return velocityActual;
	}

	private Vector2 GetCastCorrectedVelocity(Vector2 deltaPositionExpected, Vector2 expectedVelocity, bool groundCheck = false)
	{
		int count = rigidBody.Cast (deltaPositionExpected, contactFilter, hitBuffer, deltaPositionExpected.magnitude);

		Grounded = false;
		Vector2 velocityActual = expectedVelocity;
		for (int i = 0; i < count; i++) 
		{
			Vector2 currentNormal = hitBuffer[i].normal;
			
			if (groundCheck && currentNormal.y > minGroundNormalY) 
			{
				Grounded = true;
			}

			float velocityProjection = Vector2.Dot (expectedVelocity, currentNormal);
			if (velocityProjection < 0) 
			{
				velocityActual -= velocityProjection * currentNormal;
			}

			//float modifiedDistance = hitBuffer[i].distance - shellRadius;
			//distance = modifiedDistance < distance ? modifiedDistance : distance;
		}

		return velocityActual;
	}
}
