using System;
using UnityEngine;

namespace _scripts.Character
{
    public class EnemyHealthController : BaseHealthController
    {
        public event Action OnEnemyDeath;
        protected override void OnDeath()
        {
            //todo enemyManager, death anim, notify enemies left
            OnEnemyDeath?.Invoke();
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Hot hit by: " + collision.gameObject.tag);
            switch (collision.gameObject.tag)
            {
                case "BulletAk47":
                    base.TakeDamage(15);
                    return;
                case "BulletUMP-45":
                    base.TakeDamage(2);
                    return;
                case "BulletSkorpion":
                    base.TakeDamage(20);
                    return;
                default:
                    return;
            }
        }
    }
}
