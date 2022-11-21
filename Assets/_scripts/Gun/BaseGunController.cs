using System.Collections;
using _scripts.Audio;
using UnityEngine;

namespace _scripts.Gun
{
    public class BaseGunController : MonoBehaviour
    {
        public float bulletSpeed = 40;
        public GameObject bullet;
        public Transform barrel;
        public float fireRate = 2;
        public float fireTime = 1;
        [SerializeField] private bool _canShoot = true;
        private bool isTimerDone;

        private BaseAudioController _baseAudioController;


        private void Start()
        {
            _baseAudioController = GetComponent<BaseAudioController>();
        }

        public void FireBullet()
        {
            if (fireTime != 0)
            {

                if (_canShoot)
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
            var spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * barrel.forward;

            if (spawnedBullet != null) Destroy(spawnedBullet, 2);
        }

        private IEnumerator ShootAtFireRate()
        {
            _canShoot = false;
            while (!isTimerDone)
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
    }
}