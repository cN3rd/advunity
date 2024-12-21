using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Homework1
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MovingAgent : NotifierBehaviour
    {
        [SerializeField] private Transform destination;
        [SerializeField] private float speed;
        [SerializeField] private NavMeshAgent agent;

        [Header("Area Cost Override")]
        [SerializeField] private bool overrideEnabled = false;
        [SerializeField, NavMeshArea] private int areaIndex;
        [SerializeField] private float areaCost = 1;
        
        private bool _wayPointReached;

        private void Start()
        {
            agent.SetDestination(destination.position);
            if(overrideEnabled)
                agent.SetAreaCost(areaIndex, areaCost);
        }

        private void Update()
        {
            // remainingDistance is zero if the path hasn't been calculated yet
            if (agent.remainingDistance == 0) return;
            
            if (!_wayPointReached && !agent.isStopped && agent.remainingDistance <= 0.1f)
            {
                Notify($"{name} reached its destination!");
                _wayPointReached = true;
            }
        }
    }
}
