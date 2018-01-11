using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemyController : TimeStopable
{
    public float Speed;

    private bool _isCanMove;
    private IEnumerator<Vector3> _routListEnumerator;

    [SerializeField] private List<Vector3> _routList;


    // Use this for initialization
    void Start()
    {
        _isCanMove = false;
        _routListEnumerator = new InfiniteLoopEnumerator<Vector3>(_routList);
        _routListEnumerator.MoveNext();
    }

    // Update is called once per frame
    void Update() {
        _isCanMove = IsCanMove();
        if (_isCanMove) FollowRoute();
    }

    private void FollowRoute()
    {
        var direction = _routListEnumerator.Current - transform.position;
        transform.Translate(direction.normalized * Speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, _routListEnumerator.Current) < 0.1f) _routListEnumerator.MoveNext();
    }
}