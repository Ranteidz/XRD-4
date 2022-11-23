using System.Collections;
using _scripts.Audio;
using _scripts.UI;
using UnityEngine;

namespace _scripts.Character
{
    public abstract class BaseHealthController : MonoBehaviour
    {
        public int Health;
        private BaseAudioController _baseAudioController;
        private bool canPlayTakeDamage = true;

        protected virtual void Start()
        {
            _baseAudioController = GetComponent<BaseAudioController>();
            InformationUIController.SetPlayerHealth(Health);
        }

        public void PlayGtaClip()
        {
            if(_baseAudioController != null) _baseAudioController.PlayOnce("AwShit");
        }

        protected virtual void TakeDamage(int damage = 1)
        {
            Health -= damage;

            if (Health <= 0)
            {
                OnDeath();
                if (_baseAudioController != null) _baseAudioController.PlayOnce("Death");
            }
            else
            {
                if (canPlayTakeDamage)
                {
                    StartCoroutine(TakeDamage());
                }
            }
        }

        private IEnumerator TakeDamage()
        {
            canPlayTakeDamage = false;
                if (_baseAudioController != null) _baseAudioController.PlayOnce("TakeDamage");
                yield return new WaitForSeconds(1);
                canPlayTakeDamage = true;

        }

        protected abstract void OnDeath();
    }
}