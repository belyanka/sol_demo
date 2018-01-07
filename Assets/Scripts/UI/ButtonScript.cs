using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonScript : MonoBehaviour {
    
    public int SceneNumber { get; set; }

    public void ButtonClicked() {
        SceneManager.LoadScene(SceneNumber);
    }
    
}