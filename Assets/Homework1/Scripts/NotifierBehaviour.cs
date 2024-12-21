using System;
using UnityEngine;

namespace Homework1
{
    /// <summary>
    /// Wrapper class exposing the OnNotify event
    /// </summary>
    public class NotifierBehaviour : MonoBehaviour
    {
        public event Action<string> OnNotify;
        
        protected void Notify(string message) => OnNotify?.Invoke(message);
    }
}
