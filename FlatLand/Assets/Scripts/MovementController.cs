using System;
using UnityEngine;

public class MovementController : MonoBehaviour {
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float rotationSpeed;
    private float _speed;
    private Vector3 _inputDirection;
    private CharacterController _characterController;
    
    private void Start() {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        _inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (_inputDirection.magnitude < 0.1f) return;
        
        _speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        float targetAngle = Mathf.Atan2(_inputDirection.x, _inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), rotationSpeed * Time.deltaTime);

        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f).normalized * Vector3.forward;
        _characterController.Move(moveDirection * _speed * Time.deltaTime);
    }

    private void OldMovementSystem() {
        _inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        _speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_newDirection), rotationSpeed);

        transform.Translate(_inputDirection * _speed * Time.deltaTime, Space.World);
        //_direction = _newDirection;

        Debug.Log("Horiz: " + Input.GetAxis("Horizontal") + " .. Vertic: " + Input.GetAxis("Vertical"));
    }
}   