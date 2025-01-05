using UnityEngine;
using UnityEngine.Events;

namespace Homework2.Scripts
{
    public class Ob2 : MonoBehaviour
    {
        public event UnityAction SayHi;
        public event UnityAction<int> SayInteger;
        public event UnityAction<string> SayString;

        [SerializeField] private UnityEvent HitShlomo = new UnityEvent();
        [SerializeField] private UnityEvent<GameObject> HitMark = new UnityEvent<GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("OB1"))
                return;

            Ob1 ob1 = other.GetComponent<Ob1>();
            SayHi += ob1.OnHitHi;
            SayInteger += ob1.OnHitInt;
            SayString += ob1.OnHitString;

            SayHi?.Invoke();
            SayInteger?.Invoke(ob1.GetDamage());
            SayString?.Invoke(ob1.GetString());

            HitShlomo?.Invoke();
            HitMark?.Invoke(ob1.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("OB1")) return;

            Ob1 ob1 = other.GetComponent<Ob1>();

            SayHi -= ob1.OnHitHi;
            SayInteger -= ob1.OnHitInt;
            SayString -= ob1.OnHitString;
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
