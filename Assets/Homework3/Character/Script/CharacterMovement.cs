using UnityEngine;
using UnityEngine.InputSystem;

namespace Homework3.Character.Script
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Animator animator;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float playerRotationSpeed;
        [SerializeField] private float health;

        public float Health { get { return health; } }
        public Animator Animator { get { return animator; } }

        private Vector3 _playerDirection;
        private Vector2 _playerInput;
        private float _playerRotationDirection;

        private static readonly int IsCrouchHash = Animator.StringToHash("isCrouch");
        private static readonly int MoveSpeedHash = Animator.StringToHash("moveSpeed");
        private static readonly int Falling = Animator.StringToHash("StartFalling");


        private void Update()
        {
            Movement();
            PlayerRotation();
            CheckSequence();
        }
        private void Movement()
        {
            if (!characterController.enabled) return;
            
            characterController.Move(_playerDirection * (movementSpeed * Time.deltaTime));
            animator.SetFloat(MoveSpeedHash, characterController.velocity.magnitude);
        }

        private void CheckSequence()
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                animator.SetTrigger(Falling);
            }
        }

        private void PlayerRotation()
        {
            //if no changes in inputs then we don't need to keep going and change the rotation
            if (_playerInput.sqrMagnitude == 0 || !characterController.enabled)
            { return; }
            //calculate the degree of the angle that we want to look at
            float angleToRotate = Mathf.Atan2(_playerDirection.x, _playerDirection.z) * Mathf.Rad2Deg;
            //make a smooth transition between the angles - between the current angle and the new inputted angle
            float angle = Mathf.SmoothDampAngle(
                characterController.transform.eulerAngles.y,
                angleToRotate,
                ref _playerRotationDirection,
                playerRotationSpeed * Time.deltaTime
            );

            float angleDifferences = Mathf.DeltaAngle(characterController.transform.eulerAngles.y, angle);
            //actually translate the rotation
            characterController.transform.Rotate(Vector3.up, angleDifferences);
        }

        public void OnPlayerMovement(InputAction.CallbackContext context)
        {
            _playerInput = context.ReadValue<Vector2>();
            _playerDirection = new Vector3(_playerInput.x, 0, _playerInput.y);
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                animator.SetBool(IsCrouchHash, true);
                Debug.Log("Crouching");
            }
            else if(context.canceled) 
            {
                animator.SetBool(IsCrouchHash, false);
                Debug.Log("Stopped crouching");
            }
        }
    }
}
