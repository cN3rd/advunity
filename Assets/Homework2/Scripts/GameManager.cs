using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Homework2.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PrefabCollection prefabCollection;
        [SerializeField] private Enemy enemy;
        [SerializeField] private TextMeshProUGUI healthText;

        private void Start() => healthText.text = $"Mark's HP:\n{enemy.Health:N0}";

        private void Update()
        {
            // Direct use of input system since we're lazy lmao
            Keyboard keyboard = Keyboard.current;
            if (keyboard.enterKey.wasPressedThisFrame)
            {
                SpawnBullet(prefabCollection.GetRandomPrefab());
            }
            else if (keyboard.cKey.wasPressedThisFrame)
            {
                SpawnBullet(prefabCollection.GetRandomPrefab(PrefabTag.Children));
            }
            else if (keyboard.mKey.wasPressedThisFrame)
            {
                SpawnBullet(prefabCollection.GetRandomPrefab(PrefabTag.LesserMark));
            }
            else if (keyboard.tKey.wasPressedThisFrame)
            {
                SpawnBullet(prefabCollection.GetRandomPrefab(PrefabTag.Teachers));
            }
        }

        private void SpawnBullet(GameObject prefab)
        {
            // TODO: proper shooting from place
            GameObject instance = Instantiate(prefab, transform.position + new Vector3(0, 10, 0),
                Quaternion.identity);

            if (!instance.TryGetComponent(out Bullet bullet)) return;

            bullet.OnHitEnemy += OnHitEnemy;
            bullet.OnBulletCollision += OnBulletCollision;
        }

        private void OnBulletCollision(BulletCollisionEventArgs args)
        {
            args.OriginBullet.DoubleDamage();
            args.OtherBullet.DoubleDamage();
            Debug.Log("Doubled bullets damage upon collision");
        }

        private void OnHitEnemy(int damage) => enemy.DecreaseHealth(damage);

        public void UpdateStatusOnHit(EnemyHitInfo enemyHitInfo) => healthText.text =
            $"Mark got hit by {enemyHitInfo.Damage} points.\nRemaining Mark's HP:\n{enemyHitInfo.RemainingHealth:N0}";

        public void UpdateStatusOnDeath() => healthText.text = "Mark's dead, long live Eldar.";
    }
}
