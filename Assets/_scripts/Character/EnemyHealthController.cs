using System;
using System.Collections;
using _scripts.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace _scripts.Character
{
    public class EnemyHealthController : BaseHealthController
    {
        public event Action OnEnemyDeath;
        private bool isEnemyDead = false;
        protected override void OnDeath()
        {
            if (!isEnemyDead)
            {
                OnEnemyDeath?.Invoke();
            }

            isEnemyDead = true;
            //todo enemyManager, death anim, notify enemies left
            StartCoroutine(StartDeathTimer());
        }

        private void OnCollisionEnter(Collision collision)
        {
            /*Debug.Log("Hot hit by: " + collision.gameObject.tag);*/
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
        private IEnumerator StartDeathTimer()
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }
}
