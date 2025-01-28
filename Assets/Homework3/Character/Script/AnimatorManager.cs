using System;
using UnityEngine;

namespace Homework3.Character.Script
{
    public class AnimatorManager : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private static readonly int CrouchHashed = Animator.StringToHash("crouchSpeed");
        private static readonly int MoveHashed = Animator.StringToHash("moveSpeed");

        private void Start()
        {
            animator.SetFloat(CrouchHashed, 0.8f);
            animator.SetFloat(MoveHashed, 1.2f);
        }
    }
}
