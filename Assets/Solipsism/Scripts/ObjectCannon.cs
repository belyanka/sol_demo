using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCannon : TimeStopable, IButtonSubscriber {

	public Vector2 direction;
	public float force;
	public List<ButtonController> buttons;
	public Transform spawnPoint;
	public GameObject shot;
	public int poolSize;
	private List<GameObject> shotsPool;
	private bool mustShoot;
	
	// Use this for initialization
	void Start () {
		mustShoot = false;
		shotsPool = new List<GameObject>(poolSize);
		for (int i = 0; i < poolSize; i++) {
			GameObject obj = Instantiate(shot,spawnPoint.position,Quaternion.identity);
			obj.SetActive(false);
			shotsPool.Add(obj);
		}
		buttons.ForEach(b => b.Subscribe(this));
	}

	private void FixedUpdate() {
		if (mustShoot && IsCanMove()) {
			Shoot();
			mustShoot = false;
		}
	}

	void Shoot() {
		//shoot an object in direction from spawnPoint
		bool found = false;
		for (int i = 0; i < poolSize; i++) {
			Rigidbody2D rb = shotsPool[i].GetComponent<Rigidbody2D>();
			if (!shotsPool[i].activeInHierarchy && !found) {
				//shoot
				shotsPool[i].SetActive(true);			
				rb.position = spawnPoint.position;
				rb.AddForce(direction*force,ForceMode2D.Impulse);
				found = true;
			}
			else {
				shotsPool[i].GetComponent<FallingObject>().ResetStatus();
				shotsPool[i].SetActive(false);
			}
		}
	}

	public void OnButtonPressed(ButtonController b) {
		//will shoot ASAP on FixedUpdate event
		mustShoot = true;
	}

	public void OnButtonReleased(ButtonController b) {
		
	}
}
