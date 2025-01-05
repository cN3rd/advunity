using UnityEngine;

namespace Homework2.Scripts
{
    public class Ob1 : MonoBehaviour
    {
        [SerializeField] private int damage = 20;
        [SerializeField] private string name = "Gal";

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

        public int GetDamage()
        {
            return damage;
        }

        public string GetString()
        {
            return name;
        }
    }
}
