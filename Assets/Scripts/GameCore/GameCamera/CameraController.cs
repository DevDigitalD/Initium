using System;
using UnityEngine;

namespace GameCore.GameCamera
{
    public class CameraController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Transform _orientaion;
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _playerObj;
        [SerializeField] private Rigidbody _playerRb;
        [SerializeField]private float _rotationSpeed;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            Vector3 viewDir = _player.position -
                              new Vector3(transform.position.x, transform.position.y, transform.position.z);

            _orientaion.forward = viewDir.normalized;

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = _orientaion.forward * verticalInput + _orientaion.right * horizontalInput;

            if (inputDir != Vector3.zero)
            {
                _playerObj.forward =
                    Vector3.Slerp(_playerObj.forward, inputDir.normalized, Time.deltaTime * _rotationSpeed);
            }
        }
    }
}
