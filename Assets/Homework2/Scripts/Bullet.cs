using UnityEngine;
using UnityEngine.Events;

namespace Homework2.Scripts
{
    public class BulletCollisionEventArgs
    {
        public Bullet OriginBullet { get; init; }
        public Bullet OtherBullet { get; init; }
    }

    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int damage;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                OnHitEnemy?.Invoke(damage);
            }
            else if (other.gameObject.CompareTag("Bullet"))
            {
                OnBulletCollision?.Invoke(new BulletCollisionEventArgs
                {
                    OriginBullet = this, OtherBullet = other.gameObject.GetComponent<Bullet>()
                });
            }
        }

        public event UnityAction<int> OnHitEnemy;
        public event UnityAction<BulletCollisionEventArgs> OnBulletCollision;

        public void DoubleDamage() => damage *= 2;
    }
}
