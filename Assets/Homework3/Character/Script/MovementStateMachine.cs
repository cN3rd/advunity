using UnityEngine;

public class MovementStateMachine : StateMachineBehaviour
{
    private CharacterController playerController;

    public MovementStateMachine()
    {
        Debug.Log("MovementStateMachine script is being created!");
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Falling State!");
        playerController = animator.GetComponentInParent<CharacterController>();

        if (playerController != null)
        {
            playerController.enabled = false;
        }
        else
        {
            Debug.LogError("CharacterController not found!");
        }
    }


    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        Debug.Log("Exited SubState machine!");

        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }
}
