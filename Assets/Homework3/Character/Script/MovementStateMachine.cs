using UnityEngine;

namespace Homework3.Character.Script
{
    public class MovementStateMachine : StateMachineBehaviour
    {
        private CharacterController _playerController;
        private CharacterMovement _characterMovement;
        private float _playerHealth;

        private static readonly int PlayerAliveHash = Animator.StringToHash("characterAlive");
        public MovementStateMachine()
        {
            Debug.Log("MovementStateMachine script is being created!");
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("Entered Falling State!");
            _playerController = animator.GetComponentInParent<CharacterController>();
            _characterMovement = animator.GetComponentInParent<CharacterMovement>();
            if (_playerController)
            {
                _playerController.enabled = false;
            }
            else
            {
                Debug.LogError("CharacterController not found!");
            }

            _playerHealth = _characterMovement.Health;

            if (_playerHealth > 0)
            {
                _playerController.enabled = true;
                _characterMovement.Animator.SetBool(PlayerAliveHash, true);
            }
            else
            {
                _characterMovement.Animator.SetBool(PlayerAliveHash, false);
            }
        }


        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            Debug.Log("Exited SubState machine!");

            if (!_playerController || !_characterMovement) return;
        
            _playerHealth = _characterMovement.Health;
            if (_playerHealth > 0)
            {
                _playerController.enabled = true;
                _characterMovement.Animator.SetBool(PlayerAliveHash, true);
            }
            else
            {
                _characterMovement.Animator.SetBool(PlayerAliveHash, false);
            }
        }
    }
}
