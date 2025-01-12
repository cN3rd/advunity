using System.Globalization;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

namespace Homework2.Scripts
{
    public struct EnemyHitInfo
    {
        public int Damage { get; init; }
        public BigInteger RemainingHealth { get; init; }
    }

    public class Enemy : MonoBehaviour
    {
        [SerializeField] private string serializedHealth;

        [SerializeField] private UnityEvent OnDeath;
        [SerializeField] private UnityEvent<EnemyHitInfo> OnHit;

        private BigInteger _health;
        public BigInteger Health => _health;

        private void Awake()
        {
            if (BigInteger.TryParse(serializedHealth,
                    NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint |
                    NumberStyles.AllowLeadingSign,
                    CultureInfo.InvariantCulture,
                    out _health)) return;

            Debug.Log("Invalid health value! Health set to 1.");
            _health = BigInteger.One;
        }
        
        public void DecreaseHealth(int damage)
        {
            if (_health <= 0) return;

            _health -= damage;
            if (_health <= 0) OnDeath?.Invoke();
            else OnHit?.Invoke(new EnemyHitInfo { Damage = damage, RemainingHealth = _health });
        }
    }
}
