using UnityEngine;

namespace Homework2.Scripts
{
    public class Ob1 : MonoBehaviour
    {
        [SerializeField] private int damage = 20;
        [SerializeField] private string name = "Gal";

        public int Damage { get { return damage; } }
        public string Name { get { return name; } }
        public void OnHitHi()
        {
            Debug.Log("Hi!");
        }

        public void OnHitInt(int damageAmount)
        {
            Debug.Log(damageAmount);
        }

        public void OnHitString(string name)
        {
            Debug.Log(name);
        }
    }
}
