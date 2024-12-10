using System;
using Unity.VisualScripting;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Color color;

    private int _curIndex;

    private void Update()
    {
        if (wayPoints.Length <= 0) return;
        
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[_curIndex].position, speed * Time.deltaTime);
        if (Math.Abs(transform.position.x - wayPoints[_curIndex].position.x) <= 0.1f) 
            SetNextWayPoint();
    }

    private void SetNextWayPoint()
    {
        ++_curIndex;

        if (_curIndex >= wayPoints.Length)
            _curIndex = 0;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere();
    }
}
