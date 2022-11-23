using System;
using UnityEngine;

namespace _scripts.Gun
{
    public class DestroyEnemyBullet : MonoBehaviour
    {
        public GameObject bulletObject;
        // Start is called before the first frame update
        /*private void OnCollisionEnter(Collision collision)
        {
            if(!collision.gameObject.CompareTag(("Enemy")))
                Destroy(bulletObject);
        }*/

        /*private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag !="Enemy" && other.gameObject.tag != "Player")
            {
                Debug.Log("Collided with: " + other.gameObject.tag);
                Destroy(bulletObject);
            }
        }*/
    }
}
