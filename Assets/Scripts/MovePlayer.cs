using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform wayPoint;

    private bool _wayPointReached;
    private void Start()
    {
        if(wayPoint)
            SetDestination(wayPoint);
    }
    
    private void Update()
    {
        if (!_wayPointReached && !agent.isStopped && agent.remainingDistance <= 0.1f)
        {
            Debug.Log("Way Point Reached!");
            _wayPointReached = true;
        }

    }

    private void SetDestination(Transform target)
    {
        if (agent)
            agent.SetDestination(target.position);
    }
}
