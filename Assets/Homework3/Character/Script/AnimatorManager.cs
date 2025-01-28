using System;
using UnityEngine;

namespace Homework3.Character.Script
{
    public class AnimatorManager : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private static readonly int IdleHashed = Animator.StringToHash("idleSpeed");
        private static readonly int MoveHashed = Animator.StringToHash("moveSpeed");

        private void Start()
        {
            animator.SetFloat(IdleHashed, 1.5f);
            animator.SetFloat(MoveHashed, 1.2f);
        }
    }
}
