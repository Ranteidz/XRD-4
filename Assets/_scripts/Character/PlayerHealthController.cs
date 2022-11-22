using System;
using UnityEngine;

namespace _scripts.Character
{
    public class PlayerHealthController : BaseHealthController
    {
        public Action OnCharacterDeath { get; set; }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("EnemyBullet")) TakeDamage();
        }

        protected override void OnDeath()
        {
            OnCharacterDeath.Invoke();
        }
    }
}