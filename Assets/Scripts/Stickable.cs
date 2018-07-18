using UnityEngine;

public class Stickable : MonoBehaviour
{
    private const string StickySurfaceTag = "StickySurface";
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StickySurfaceTag))
        {
            transform.parent = other.transform;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StickySurfaceTag))
        {
            transform.parent = null;
        }
    }
}