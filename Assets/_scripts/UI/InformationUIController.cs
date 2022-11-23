using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _scripts.UI
{
    public class InformationUIController : MonoBehaviour
    {
        private static TMP_Text _playerHealth;
        private static TMP_Text _enemiesLeft;
        private static readonly string health = "Player health: ";
        private static readonly string enemies = "Enemies left: ";

        public XRController rightController;
        public InputHelpers.Button AButton;
        public GameObject canvas;

        private void Start()
        {
            _playerHealth = GameObject.Find("PlayerHealthTMP").GetComponent<TMP_Text>();
            _enemiesLeft = GameObject.Find("EnemiesLeftTMP").GetComponent<TMP_Text>();
            _playerHealth.SetText(health);
            _enemiesLeft.SetText(enemies);
        }

        private void Update()
        {
            if (CheckIfActivated(rightController))
            {
                canvas.SetActive(true);
            }else canvas.SetActive(false);
        }

        public static void SetPlayerHealth(int playerHealth)
        {
            _playerHealth.SetText(health + playerHealth);
        }

        public static void SetEnemiesLeft(int enemiesLeft)
        {
            _enemiesLeft.SetText(enemies + enemiesLeft);
        }

        private bool CheckIfActivated(XRController xrController)
        {
            xrController.inputDevice.IsPressed(AButton, out var isActivated);
            return isActivated;
        }
    }
}
