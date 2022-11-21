using _scripts.Audio;
using UnityEngine;

namespace _scripts.Character
{
    public abstract class BaseHealthController : MonoBehaviour
    {
        public int Health;
        private BaseAudioController _baseAudioController;

        protected virtual void Start()
        {
            _baseAudioController = GetComponent<BaseAudioController>();
        }

        protected void TakeDamage(int damage = 1)
        {
            Health -= damage;


            if (Health <= 0)
            {
                OnDeath();
                if (_baseAudioController != null) _baseAudioController.PlayOnce("Death");
            }
            else
            {
                if (_baseAudioController != null) _baseAudioController.PlayOnce("TakeDamage");
            }
        }

        protected abstract void OnDeath();
    }
}