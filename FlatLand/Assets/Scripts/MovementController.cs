using System;
using UnityEngine;

public class MovementController : MonoBehaviour {
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 _direction;
    private Vector3 _newDirection;
    private float _speed;
    
    private void Start() {
        _direction = Vector3.forward;
    }

    private void Update() {
        _newDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        _speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_newDirection), rotationSpeed);
        
        transform.Translate(_newDirection * _speed * Time.deltaTime, Space.World);
        _direction = _newDirection;

        Debug.Log("Horiz: " + Input.GetAxis("Horizontal") + " .. Vertic: " + Input.GetAxis("Vertical"));
    }
}   