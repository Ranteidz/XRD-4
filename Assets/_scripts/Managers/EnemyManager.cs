using System;
using System.Collections.Generic;
using _scripts.Character;
using _scripts.UI;
using UnityEngine;

namespace _scripts.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] public List<GameObject> enemies;
        private int _numberOfEnemies;


        private void Start()
        {
            _numberOfEnemies = enemies.Count;
            InformationUIController.SetEnemiesLeft(_numberOfEnemies);
            foreach (var enemy in enemies)
            {
                var x =  enemy.GetComponent<EnemyHealthController>();
                x.OnEnemyDeath += () =>
                {
                    _numberOfEnemies--;
                    InformationUIController.SetEnemiesLeft(_numberOfEnemies);
                };
            }
        }

        private void Update()
        {
            if (_numberOfEnemies <= 0)
            {
                Debug.Log("All enemies died");
                LevelManager.ReloadScene();
            }
        }
    }
}
