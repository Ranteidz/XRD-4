using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace _Scripts
{
    public class HandPresence : MonoBehaviour
    {
        public bool showController;
        public InputDeviceCharacteristics controllerCharacteristics;
        public List<GameObject> controllerPrefabs;
        public GameObject handModelPrefab;
        private Animator _handAnimator;
        private GameObject _spawnedController;
        private GameObject _spawnedHandModel;
        private InputDevice _targetDevice;
        
        private void Start()
        {
            TryInitialize();
        }

        private void Update()
        {
            if (!_targetDevice.isValid)
            {
                TryInitialize();
            }
            else
            {
                if (showController)
                {
                    _spawnedHandModel.SetActive(false);
                    _spawnedController.SetActive(true);
                }
                else
                {
                    _spawnedHandModel.SetActive(true);
                    _spawnedController.SetActive(false);
                    UpdateHandAnimation();
                }
            }
        }

        private void TryInitialize()
        {
            var devices = new List<InputDevice>();

            InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

            foreach (var item in devices) Debug.Log(item.name + item.characteristics);

            if (devices.Count > 0)
            {
                _targetDevice = devices[0];
                var prefab = controllerPrefabs.Find(controller => controller.name == _targetDevice.name);
                if (prefab)
                {
                    _spawnedController = Instantiate(prefab, transform);
                }
                else
                {
                    Debug.LogError("Error");
                    _spawnedController = Instantiate(controllerPrefabs[0], transform);
                }

                _spawnedHandModel = Instantiate(handModelPrefab, transform);
                _handAnimator = _spawnedHandModel.GetComponent<Animator>();
            }
        }

        private void UpdateHandAnimation()
        {
            if (_targetDevice.TryGetFeatureValue(CommonUsages.trigger, out var triggerValue))
                _handAnimator.SetFloat("Trigger", triggerValue);
            else
                _handAnimator.SetFloat("Trigger", 0);
            if (_targetDevice.TryGetFeatureValue(CommonUsages.grip, out var gripValue))
                _handAnimator.SetFloat("Grip", gripValue);
            else
                _handAnimator.SetFloat("Grip", 0);
        }
    }
}