using UnityEngine;

namespace _scripts.Gun
{
    public class DestroyOnCollision : MonoBehaviour
    {
        public GameObject bulletObject;
        // Start is called before the first frame update
        private void OnCollisionEnter(Collision collision)
        {
            
            if(!collision.gameObject.CompareTag(("Weapon")))
                Destroy(bulletObject);
        }
    }
}
