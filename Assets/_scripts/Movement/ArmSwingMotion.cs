using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Scripts
{
    public class ArmSwingMotion : MonoBehaviour
    {
        public GameObject LeftHand;
        public GameObject RightHand;
        public GameObject CenterEyeCamera;
        public GameObject ForwardDirection;
        public XRController LeftController;
        public InputHelpers.Button ArmSwingButton;
        public float speed = 70;
        private float handSpeed;
        private Vector3 PlayerPosPrevFrame;
        private Vector3 PlayerPosThisFrame;

        private Vector3 PosPrevFrameLeftHand;
        private Vector3 PosPrevFramRightHand;
        private Vector3 PosThisFrameLeftHand;
        private Vector3 PosThisFrameRightHand;


        // Start is called before the first frame update
        private void Start()
        {
            PlayerPosPrevFrame = transform.position;
            PosPrevFrameLeftHand = LeftHand.transform.position;
            PosThisFrameRightHand = RightHand.transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            var yRotation = CenterEyeCamera.transform.eulerAngles.y;
            ForwardDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);

            PosThisFrameLeftHand = LeftHand.transform.position;
            PosThisFrameRightHand = RightHand.transform.position;

            PlayerPosThisFrame = transform.position;

            var playerDistanceMoved = Vector3.Distance(PlayerPosThisFrame, PlayerPosPrevFrame);
            var leftHandDistanceMoved = Vector3.Distance(PosPrevFrameLeftHand, PosThisFrameLeftHand);
            var rightHandDistanceMoved = Vector3.Distance(PosPrevFramRightHand, PosThisFrameRightHand);

            handSpeed = leftHandDistanceMoved - playerDistanceMoved +
                        (rightHandDistanceMoved - playerDistanceMoved);

            if (Time.timeSinceLevelLoad > 1f && CheckIfActivated(LeftController))
                transform.position += ForwardDirection.transform.forward * handSpeed * speed * Time.deltaTime;

            PosPrevFrameLeftHand = PosThisFrameLeftHand;
            PosPrevFramRightHand = PosThisFrameRightHand;
            PlayerPosPrevFrame = PlayerPosThisFrame;
        }

        private bool CheckIfActivated(XRController xrController)
        {
            xrController.inputDevice.IsPressed(ArmSwingButton, out var isActivated);
            return isActivated;
        }
    }
}