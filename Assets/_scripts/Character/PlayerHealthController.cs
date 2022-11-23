using System;
using System.Collections;
using _scripts.Managers;
using _scripts.UI;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _scripts.Character
{
    public class PlayerHealthController : BaseHealthController
    {
        protected override void Start()
        {
            base.Start();
            PlayGtaClip();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("EnemyBullet")) TakeDamage();
            InformationUIController.SetPlayerHealth(base.Health);
        }

        protected override void OnDeath()
        {
            StartCoroutine(StartDeathTimer());
        }

        private IEnumerator StartDeathTimer()
        {
            yield return new WaitForSeconds(2);
            LevelManager.ReloadScene();
        }
    }
}