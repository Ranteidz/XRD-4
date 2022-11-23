using System;
using _scripts.Managers;
using UnityEngine;

namespace _scripts.Character
{
    public class PlayerHealthController : BaseHealthController
    {
        public Action OnCharacterDeath { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("EnemyBullet")) TakeDamage();
        }

        protected override void OnDeath()
        {
            LevelManager.ReloadScene();
        }
    }
}