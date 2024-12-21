using JetBrains.Annotations;
using UnityEngine;

namespace Homework1
{
    public class MovingObstacle : MonoBehaviour
    {
        [SerializeField] private Transform location1;
        [SerializeField] private Transform location2;
        [SerializeField] private float speed = 5f;
        [SerializeField, HideInInspector] private Bounds obstacleBounds;
        
        private Vector3 _currentTargetPosition;
        private Transform _targetLocation;

        private void Start()
        {
            _targetLocation = location1;
            CalculateTargetPosition();
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _currentTargetPosition,
                speed * Time.deltaTime
            );

            if (!(Vector3.Distance(transform.position, _currentTargetPosition) < 0.01f)) return;

            _targetLocation = _targetLocation == location1 ? location2 : location1;
            CalculateTargetPosition();
        }

        private void CalculateTargetPosition()
        {
            int direction = _targetLocation == location1 ? 1 : -1;
            Vector3 bodyOffset = direction * new Vector3(0, 0, obstacleBounds.size.z / 2);
            _currentTargetPosition = _targetLocation.position - bodyOffset;
        }

#if UNITY_EDITOR
        private void OnValidate() => obstacleBounds = CalculateBounds();

        [UsedImplicitly]
        private Bounds CalculateBounds()
        {
            var renderers = GetComponentsInChildren<Renderer>(true);
            if (renderers.Length == 0)
                return new Bounds(transform.position, Vector3.zero);

            Bounds combinedBounds = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++)
            {
                combinedBounds.Encapsulate(renderers[i].bounds);
            }

            return combinedBounds;
        }
#endif
    }
}
