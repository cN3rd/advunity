using UnityEngine;

namespace Homework1
{
    public class MovingObstacle : MonoBehaviour
    {
        [SerializeField] private Transform location1;
        [SerializeField] private Transform location2;
        [SerializeField] private float speed = 5f;

        private Transform _targetLocation;

        private void Start() =>
            _targetLocation = location1;

        private void Update()
        {
            // Move the NavMeshObstacle towards the target location
            transform.position = Vector3.MoveTowards(transform.position, _targetLocation.position,
                speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _targetLocation.position) < 0.1f)
            {
                // Switch target location when the obstacle reaches the current target
                _targetLocation = _targetLocation == location1 ? location2 : location1;
            }
        }
    }
}
