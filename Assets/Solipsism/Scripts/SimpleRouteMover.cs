using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class SimpleRouteMover : TimeStopable
{
    public float Speed;
    public float pauseTime;
    public bool directionSpriteFlip = false;

    private int directionX = 1;
    private SpriteRenderer spriteRenderer;
    private IEnumerator<Vector3> _routListEnumerator;

    [SerializeField] private List<Vector3> _routeList;

    // Use this for initialization
    void Start() {
        if (directionSpriteFlip) {
            spriteRenderer=GetComponentInChildren<SpriteRenderer> ();
        }
        _routListEnumerator = new InfiniteLoopEnumerator<Vector3>(_routeList);
        _routListEnumerator.MoveNext();
        StartCoroutine("FollowRoute");
    }

    IEnumerator FollowRoute()
    {
        while (true)
        {
            if (IsCanMove())
            {
                var direction = _routListEnumerator.Current - transform.position;
                if (directionSpriteFlip) {
                    if (directionX*direction.x < 0) 
                    {
                        spriteRenderer.flipX = !spriteRenderer.flipX;
                        directionX *= -1;
                    }
                }
                transform.Translate(direction.normalized * Speed * Time.deltaTime, Space.World);
                if (Vector3.Distance(transform.position, _routListEnumerator.Current) < 0.1f)
                {
                    if (pauseTime > 0)
                    {
                        yield return new WaitForSeconds(pauseTime);
                    }
                    _routListEnumerator.MoveNext();
                }    
            }
            yield return null;    
        }   
    }
}