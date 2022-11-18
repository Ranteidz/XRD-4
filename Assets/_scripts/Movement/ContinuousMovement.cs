using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

namespace _Scripts
{
    public class ContinuousMovement : MonoBehaviour
    {
        public XRNode inputSource;
        public float speed = 1;
        public float additionalHeight = 0.2f;
        private CharacterController _character;
        private Vector2 _inputAxis;
        private XROrigin _rig;
       
        private void Start()
        {
            _character = GetComponent<CharacterController>();
            _rig = GetComponent<XROrigin>();
        }
       
        private void Update()
        {
            var device = InputDevices.GetDeviceAtXRNode(inputSource);
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out _inputAxis);
        }

        private void FixedUpdate()
        {
            FollowHeadset();
            var headYaw = Quaternion.Euler(0, _rig.Camera.transform.eulerAngles.y, 0);
            var direction = headYaw * new Vector3(_inputAxis.x, 0, _inputAxis.y);

            _character.Move(direction * Time.fixedDeltaTime * speed);
        }

        private void FollowHeadset()
        {
            _character.height = _rig.CameraInOriginSpaceHeight + additionalHeight;
            var capsuleCenter = transform.InverseTransformPoint(_rig.Origin.transform.position);
            _character.center =
                new Vector3(capsuleCenter.x, _character.height / 2 + _character.skinWidth, capsuleCenter.z);
        }
    }
}