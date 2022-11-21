using System;
using UnityEngine;

namespace _scripts.Character
{
    public class PlayerHealthController : BaseHealthController
    {
        protected override void OnDeath()
        {
            //Todo reset level
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("EnemyBullet"))
            {
                TakeDamage();
            }
        }
    }
}
