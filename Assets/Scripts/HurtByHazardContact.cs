using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtByHazardContact : MonoBehaviour {

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Hazard")) {
            if (gameObject.CompareTag("Player")) {
                gameObject.GetComponent<PlayerController>().SetDead();
            }
            else {
                gameObject.SetActive(false);
            }
        }
    }
}