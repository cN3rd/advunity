using UnityEngine;

public class MovementStateMachine : StateMachineBehaviour
{
    private CharacterController playerController;
    private CharacterMovement characterMovement;
    private float playerHealth;

    private static readonly int playerAliveHash = Animator.StringToHash("characterAlive");
    public MovementStateMachine()
    {
        Debug.Log("MovementStateMachine script is being created!");
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Falling State!");
        playerController = animator.GetComponentInParent<CharacterController>();
        characterMovement = animator.GetComponentInParent<CharacterMovement>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }
        else
        {
            Debug.LogError("CharacterController not found!");
        }

        playerHealth = characterMovement.Health;

        if (playerHealth > 0)
        {
            playerController.enabled = true;
            characterMovement.Animator.SetBool(playerAliveHash, true);
        }
        else
        {
            characterMovement.Animator.SetBool(playerAliveHash, false);
        }
    }


    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        Debug.Log("Exited SubState machine!");

        if (playerController != null && characterMovement != null)
        {
            playerHealth = characterMovement.Health;
            if (playerHealth > 0)
            {
                playerController.enabled = true;
                characterMovement.Animator.SetBool(playerAliveHash, true);
            }
            else
            {
                characterMovement.Animator.SetBool(playerAliveHash, false);
            }
        }
    }
}
