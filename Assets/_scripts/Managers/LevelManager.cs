using _scripts.Character;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private PlayerHealthController _player;


        private void Start()
        {
            _player = GetComponent<PlayerHealthController>();
        }

        private void ReloadLevel()
        {
            _player.OnCharacterDeath = () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            };
        }
    }
}
