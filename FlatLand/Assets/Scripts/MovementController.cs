using System;
using UnityEngine;

public class MovementController : MonoBehaviour {
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float rotationSpeed;
    private CharacterController _characterController;
    private AnimationController _animationController;
    private float _speed;
    private Vector3 _inputDirection;
    private bool _isSprinting;
    
    private void Start() {
        _characterController = GetComponent<CharacterController>();
        _animationController = GetComponent<AnimationController>();
        _animationController.SetAnimationState(PlayerState.Idle);
    }

    private void Update() {
        _inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        _isSprinting = Input.GetKey(KeyCode.LeftShift);
        _speed = _isSprinting ? sprintSpeed : moveSpeed;
    }

    private void FixedUpdate() {
        if (_inputDirection.magnitude < 0.1f) {
            _animationController.SetAnimationState(PlayerState.Idle);
            return;
        }
        
        _animationController.SetAnimationState(_isSprinting ? PlayerState.Sprinting : PlayerState.Running);

        float targetAngle = Mathf.Atan2(_inputDirection.x, _inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), rotationSpeed * Time.deltaTime);

        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f).normalized * Vector3.forward;
        _characterController.Move(moveDirection * _speed * Time.deltaTime);
    }
}   