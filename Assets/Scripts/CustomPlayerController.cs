using CnControls;
using UnityEngine;

public class CustomPlayerController : CustomPhysicsObject {

	public float walkingSpeed = 7;
	public float jumpTakeOffSpeed = 7;
	public GameController gameController;

	// Use this for initialization
	void Awake ()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	protected override Vector2 GetExpectedVelocity()
	{
		//todo: разобраться, почему не всегда срабатывает прыжок
		//todo: возможно стоит перенести рассчет expectedVelocity в Update() и хранить в поле, здесь только возвращать
		Vector2 expectedVelocity = base.GetExpectedVelocity();
				
		expectedVelocity.x = walkingSpeed * CnInputManager.GetAxisRaw("Horizontal");
		
 		if (CnInputManager.GetButtonDown("Jump") && Grounded) {
			expectedVelocity.y = jumpTakeOffSpeed;
		} 
		else if (CnInputManager.GetButtonUp ("Jump")) 
		{
			if (VelocityCurrent.y > 0) {
				expectedVelocity.y = VelocityCurrent.y * 0.5f;
			}
		}

		return expectedVelocity; //move * maxSpeed;
	}
	
	public void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			SetDead();
		}
	}

	public void SetDead() {
		gameController.GameOver();
	}
}
