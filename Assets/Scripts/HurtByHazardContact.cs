using UnityEngine;

public class HurtByHazardContact : MonoBehaviour {

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Hazard")) {
             if (gameObject.CompareTag("Player")) {
                gameObject.GetComponent<SimplePlayerController>().SetDead();
            }
            else {
                gameObject.SetActive(false);
            }
        }
    }
}