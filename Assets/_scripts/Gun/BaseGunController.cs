using System;
using UnityEngine;

namespace _scripts.Gun
{
    public class BaseGunController : MonoBehaviour
    {
        public float bulletSpeed = 40;
        public GameObject bullet;
        public Transform barrel;

        private BaseAudioController _baseAudioController;


        private void Start()
        {
            _baseAudioController = GetComponent<BaseAudioController>();
        }

        public void FireBullet()
        {
            GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * barrel.forward;
            Destroy(spawnedBullet, 2);
        }
    }
}
