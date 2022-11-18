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
        public float gravity = -9.81f;
        private float _fallingSpeed;
        public LayerMask groundLayer;
       
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

            bool isGrounded = checkIfGrounded();

            if (isGrounded)
            {
                _fallingSpeed = 0;
            }
            else
            {
                _fallingSpeed += gravity * Time.fixedDeltaTime;
            }
            
            _character.Move(Vector3.up * _fallingSpeed * Time.fixedDeltaTime);
        }

        private void FollowHeadset()
        {
            _character.height = _rig.CameraInOriginSpaceHeight + additionalHeight;
            var capsuleCenter = transform.InverseTransformPoint(_rig.Origin.transform.position);
            _character.center =
                new Vector3(capsuleCenter.x, _character.height / 2 + _character.skinWidth, capsuleCenter.z);
        }

        private bool checkIfGrounded()
        {
            Vector3 rayStart = transform.TransformPoint(_character.center);
            float rayLenght = _character.center.y + 0.01f;
            bool hasHit = Physics.SphereCast(rayStart, _character.radius, Vector3.down, out RaycastHit hitInfo,
                rayLenght, groundLayer);
            return hasHit;
        }
    }
}