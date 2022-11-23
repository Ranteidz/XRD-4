using System;
using UnityEngine;

namespace _scripts.Gun
{
    public class DestroyPlayerBullet : MonoBehaviour
    {
        public GameObject bulletObject;
        // Start is called before the first frame update
        private void OnCollisionEnter(Collision collision)
        {
            if(!collision.gameObject.CompareTag(("Weapon")))
                Destroy(bulletObject);
        }

        /*private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Untagged"))
            {
                Destroy(this);
            }
        }*/
    }
}
