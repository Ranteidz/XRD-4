using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Scripts
{
    public class TeleportOnToggle : MonoBehaviour
    {
        public XRController rightTeleportRay;
        public InputHelpers.Button teleportActivationButton;
        public float activationThreshold = 0.1f;
      
        private void Update()
        {
            if (rightTeleportRay) rightTeleportRay.gameObject.SetActive(CheckIfActivated(rightTeleportRay));
        }

        public bool CheckIfActivated(XRController controller)
        {
            controller.inputDevice.IsPressed(teleportActivationButton, out var isActivated, activationThreshold);
            return isActivated;
        }
    }
}