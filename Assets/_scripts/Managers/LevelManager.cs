using UnityEngine;
using UnityEngine.SceneManagement;

namespace _scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}