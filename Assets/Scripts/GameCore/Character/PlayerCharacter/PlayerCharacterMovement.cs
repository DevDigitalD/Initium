using UnityEngine;

namespace GameCore.Character.PlayerCharacter
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerCharacterMovement : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _speed;
        [SerializeField] private float _turnSmoothTime = 0.1f;
        private CharacterController _characterController;
        private float _turnSmoothVelocity;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
                float angel = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref _turnSmoothVelocity,
                        _turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angel, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngel, 0f) * Vector3.forward;
                _characterController.Move(moveDir.normalized * _speed * Time.deltaTime);
            }
        }
    }
}
