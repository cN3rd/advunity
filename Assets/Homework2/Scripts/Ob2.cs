using UnityEngine;
using UnityEngine.Events;

namespace Homework2.Scripts
{
    public class Ob2 : MonoBehaviour
    {
        [SerializeField] private ObjectsManager objectsManager;

        public event UnityAction SayHi;
        public event UnityAction<int> SayInteger;
        public event UnityAction<string> SayString;

        [SerializeField] private UnityEvent HitShlomo = new UnityEvent();
        [SerializeField] private UnityEvent<GameObject> HitMark = new UnityEvent<GameObject>();


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("OB1"))
            {
                SayHi.Invoke();
                SayInteger.Invoke(objectsManager.Damage);
                SayString.Invoke(objectsManager.Name);
            }
        }

        public void OnHitShlomo()
        {
            Debug.Log("Hi Shlomo, we kinda hit you here");
        }

        public void OnHitMark(GameObject gameObject)
        {
            Debug.Log("Hey it's Mark, sorry for hitting you");
            Debug.Log(gameObject.name);
        }
    }
}
