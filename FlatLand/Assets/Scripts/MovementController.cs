using System;
using UnityEngine;

public class MovementController : MonoBehaviour {
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] [Range(0.01f, 1.0f)] private float acceleration;
    [SerializeField] [Range(0.01f, 1.0f)] private float deceleration;
    [SerializeField] private float rotationSpeed;
    private Rigidbody _rigidbody;
    private CharacterController _characterController;
    private AnimationController _animationController;
    private float _speed;
    private float _maxSpeed;
    private Vector3 _inputDirection;
    private float _inputMagnitude;
    private bool _isSprinting;
    
    private void Start() {
        _characterController = GetComponent<CharacterController>();
        _animationController = GetComponent<AnimationController>();
        _rigidbody = GetComponent<Rigidbody>();
        _animationController.SetAnimationState(PlayerState.Idle);
    }

    private void Update() {
        _inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _inputMagnitude = _inputDirection.magnitude;
        _inputDirection = _inputDirection.normalized;
        _isSprinting = Input.GetKey(KeyCode.LeftShift);
    }

    private void FixedUpdate() {
        _animationController.SetVelocity(_inputMagnitude);
        Debug.Log(_inputMagnitude);
        
        if (_inputMagnitude < 0.1f) {
            //_speed = Decelerate(0f, deceleration);
            //_animationController.SetVelocity(_speed/(moveSpeed));
            //Debug.Log(_speed/(_input_Magnitude));
            _speed = 0;
            return;
        }
        
        _maxSpeed = _isSprinting ? sprintSpeed : moveSpeed;
        _speed = _maxSpeed * _inputMagnitude;
        
        float targetAngle = Mathf.Atan2(_inputDirection.x, _inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), rotationSpeed * Time.deltaTime);

        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f).normalized * Vector3.forward;
        _characterController.Move(moveDirection * _speed * Time.deltaTime);
    }

    private float Accelerate(float targetSpeed, float accelerationFactor) {
        return Mathf.Lerp(_speed, targetSpeed, accelerationFactor * Time.deltaTime);
    }
    private float Decelerate(float targetSpeed, float decelerationFactor) {
        return Mathf.Lerp(_speed, targetSpeed, decelerationFactor * Time.deltaTime);
    }
    
    
}   