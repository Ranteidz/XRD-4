using _scripts.Audio;
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
            var spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * barrel.forward;

            if (spawnedBullet != null) Destroy(spawnedBullet, 2);
        }
    }
}