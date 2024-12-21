using UnityEngine;
using UnityEngine.AI;

namespace Homework1
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MovingAgent : MonoBehaviour
    {
        [SerializeField] private Transform destination;
        [SerializeField] private float speed;
        [SerializeField] private NavMeshAgent agent;

        [Header("Area Cost Override")]
        [SerializeField] private bool overrideEnabled = false;
        [SerializeField, NavMeshArea] private int areaIndex;
        [SerializeField] private float areaCost = 1; 
        
        private void Start()
        {
            agent.SetDestination(destination.position);
            
            if(overrideEnabled)
                agent.SetAreaCost(areaIndex, areaCost);
        }
    }
}
