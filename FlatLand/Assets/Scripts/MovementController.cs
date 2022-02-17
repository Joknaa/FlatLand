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
    private Vector3 _inputDirection;
    private float _input_Magnitude;
    private bool _isSprinting;
    
    private void Start() {
        _characterController = GetComponent<CharacterController>();
        _animationController = GetComponent<AnimationController>();
        _rigidbody = GetComponent<Rigidbody>();
        _animationController.SetAnimationState(PlayerState.Idle);
    }

    private void Update() {
        _inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _input_Magnitude = _inputDirection.magnitude;
        _inputDirection = _inputDirection.normalized;
        _isSprinting = Input.GetKey(KeyCode.LeftShift);
    }

    private void FixedUpdate() {
        _animationController.SetVelocity(_input_Magnitude);
        Debug.Log(_input_Magnitude);
        
        if (_input_Magnitude < 0.1f) {
            //_speed = Decelerate(0f, deceleration);
            //_animationController.SetVelocity(_speed/(moveSpeed));
            //Debug.Log(_speed/(_input_Magnitude));
            _speed = 0;
            _animationController.SetAnimationState(PlayerState.Idle);
            return;
        }
        
        //_speed = Accelerate(_isSprinting ? sprintSpeed : moveSpeed, acceleration);
        _speed = (_isSprinting ? sprintSpeed : moveSpeed) * _input_Magnitude;
        
        _animationController.SetAnimationState(_isSprinting ? PlayerState.Sprinting : PlayerState.Running);

        float targetAngle = Mathf.Atan2(_inputDirection.x, _inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), rotationSpeed * Time.deltaTime);

        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f).normalized * Vector3.forward;
        _characterController.Move(moveDirection * _speed * Time.deltaTime);
        
        //Debug.Log(_characterController.velocity.magnitude/(_isSprinting ? sprintSpeed : moveSpeed));
        //_animationController.SetVelocity(_characterController.velocity.magnitude/(moveSpeed));

    }

    private float Accelerate(float targetSpeed, float accelerationFactor) {
        return Mathf.Lerp(_speed, targetSpeed, accelerationFactor * Time.deltaTime);
    }
    private float Decelerate(float targetSpeed, float decelerationFactor) {
        return Mathf.Lerp(_speed, targetSpeed, decelerationFactor * Time.deltaTime);
    }
    
    
}   