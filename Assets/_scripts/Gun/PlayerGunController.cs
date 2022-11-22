using System;
using System.Collections;
using _scripts.Audio;
using UnityEngine;

namespace _scripts.Gun
{
    public class PlayerGunController : MonoBehaviour
    {
        public float bulletSpeed = 40;
        public GameObject bullet;
        public Transform barrel;
        public GameObject bulletRotator;
        public float fireRate = 2;
        public float fireTime = 1;
        public int maxAmmo = 10;
        public int currentAmmo;
        public float reloadTime =2f;
        [SerializeField] private bool _canShoot = true;
        private bool _isReloadDone = true;
        private BaseAudioController _baseAudioController;
        private bool isTimerDone;
        private Animator _animator;

        private void Start()
        {
            _baseAudioController = GetComponent<BaseAudioController>();
            currentAmmo = maxAmmo;
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (currentAmmo <= 0)
            {
                StartCoroutine(StartReloading());
            }
        }

        public void FireBullet()
        {
            if (fireTime != 0)
            {
                if (_canShoot && _isReloadDone)
                {
                    StartCoroutine(StartFireTimer());
                    StartCoroutine(ShootAtFireRate());

                    if (_baseAudioController != null) _baseAudioController.PlayOnce("Shoot");
                }
            }
            else
            {
                SpawnBullet();
                if (_baseAudioController != null) _baseAudioController.PlayOnce("Shoot");
            }
        }

        private void SpawnBullet()
        {
            {
                var spawnedBullet = Instantiate(bullet, barrel.position, bulletRotator.gameObject.transform.rotation);
                currentAmmo--;
                spawnedBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * barrel.forward;

                if (spawnedBullet != null) Destroy(spawnedBullet, 2);
            }
        }

        private IEnumerator ShootAtFireRate()
        {
            _canShoot = false;
            while (!isTimerDone && _isReloadDone)
            {
                SpawnBullet();
                yield return new WaitForSeconds(fireRate / 10f);
            }

            _canShoot = true;
        }

        private IEnumerator StartFireTimer()
        {
            isTimerDone = false;

            yield return new WaitForSeconds(fireTime);

            isTimerDone = true;
        }

        private IEnumerator StartReloading()
        {
            _canShoot = false;
            _isReloadDone = false;
            _baseAudioController.StopPlayingSound();
            _animator.SetTrigger("Reload");
            _baseAudioController.PlayOnce("Reload");
            yield return new WaitForSeconds(reloadTime);
            _animator.ResetTrigger("Reload");
            currentAmmo = maxAmmo;
            _isReloadDone = true;
            _canShoot = true;
        }
    }
}