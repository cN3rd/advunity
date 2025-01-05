using System;
using UnityEngine;
using UnityEngine.Events;

namespace Homework2.Scripts
{
    public class PrefabManager : MonoBehaviour
    {
        public event UnityAction SayHi;
        public event UnityAction<int> SayInteger;
        public event UnityAction<string> SayString;
        
        [SerializeField] private Ob1 ob1;
        [SerializeField] private Ob2 ob2;

        [SerializeField] private UnityEvent HitShlomo;
        [SerializeField] private UnityEvent<GameObject> HitMark;
    }
}
