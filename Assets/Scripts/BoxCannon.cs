using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCannon : TimeStopable, IButtonSubscriber {

	public Vector2 direction;
	public float force;
	public List<ButtonController> buttons;
	public Transform spawnPoint;
	public GameObject shot;
	public int poolSize;
	private List<GameObject> shotsPool;
	private bool canShoot;
	
	// Use this for initialization
	void Start () {
		canShoot = false;
		shotsPool = new List<GameObject>(poolSize);
		for (int i = 0; i < poolSize; i++) {
			GameObject obj = Instantiate(shot,spawnPoint.position,Quaternion.identity);
			obj.SetActive(false);
			shotsPool.Add(obj);
		}
		buttons.ForEach(b => b.Subscribe(this));
	}

	private void Update() {
		if (canShoot) {
			Shoot();
		}
	}

	void Shoot() {
		if (IsCanMove()) {
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
			canShoot = false;
		}
	}

	public void OnButtonPressed(ButtonController b) {
		//shoot
		canShoot = true;
	}

	public void OnButtonReleased(ButtonController b) {
		
	}
}
