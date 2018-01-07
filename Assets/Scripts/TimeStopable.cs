using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopable : MonoBehaviour {

	public Collider2D TimeSphere;

	protected bool IsCanMove() {
		return Vector3.Distance(transform.position, TimeSphere.transform.position) <= TimeSphere.bounds.size.x / 2;
	}
}
