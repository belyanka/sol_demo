using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObject : MonoBehaviour {

	private bool canBePulled;
	private float oldXPosition;
	private float oldXVelocity;
	private float rayDistance;
	public LayerMask rayMask;
	
	
	// Use this for initialization
	void Start ()
	{
		rayDistance = GetComponent<Collider2D>().bounds.size.x/2 + 0.05f;
		canBePulled = false;
		if(rayMask.value==0) rayMask=LayerMask.GetMask("Default","Ground");
		oldXPosition = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (CheckCanBePulled())
		{
			oldXPosition = transform.position.x;
		}
		else
		{
			transform.position = new Vector2(oldXPosition,transform.position.y);
		}
	}


	private bool CheckCanBePulled()
	{
		//raycast влево и вправо
		Physics2D.queriesStartInColliders = false;
		RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, rayDistance,rayMask);
		RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, rayDistance,rayMask);
		Collider2D upHit = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y+1f), 0.4f, rayMask);
	
		if (upHit!=null)
		{
			return false;
		}
		//если только игрок касается, то разрешить толкание или не касается никто
		if (leftHit.collider!=null && leftHit.collider.gameObject.CompareTag("Player") && rightHit.collider==null || 
		    rightHit.collider!=null && rightHit.collider.gameObject.CompareTag("Player") && leftHit.collider==null) 
		    //rightHit.collider==null && leftHit.collider==null)
		{
			return true;
		}
		return false;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere(new Vector3(transform.position.x,transform.position.y+1f,0), 0.4f);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left*transform.localScale.x * rayDistance);
		Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right*transform.localScale.x * rayDistance);
		
	}

}
