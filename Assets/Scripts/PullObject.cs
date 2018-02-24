using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObject : MonoBehaviour {

	private float rayDistance;
	private Rigidbody2D rb;
	public LayerMask rayMask;
	
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		rayDistance = GetComponent<Collider2D>().bounds.size.x/2 + 0.05f;
		if(rayMask.value==0) rayMask=LayerMask.GetMask("Default","Ground");
	}
	

	private void FixedUpdate()
	{
		//raycast влево и вправо
		Physics2D.queriesStartInColliders = false;
		RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, rayDistance,rayMask);
		RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, rayDistance,rayMask);
		//todo: не толкать ящик, наверху которого что-то есть
		//Collider2D upHit = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y+1f), 0.4f, rayMask);
	
		//если только игрок касается, то разрешить толкание или не касается никто
		if (leftHit.collider!=null && leftHit.collider.gameObject.CompareTag("Player") && rightHit.collider==null)
		{
			rb.AddForce(Vector2.right,ForceMode2D.Impulse);
			
		}
		else if(rightHit.collider!=null && rightHit.collider.gameObject.CompareTag("Player") && leftHit.collider==null)
		{
			rb.AddForce(Vector2.left,ForceMode2D.Impulse);
		}
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
