using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    [SerializeField] private bool shouldRotate;
    [SerializeField] private float rotationSpeed;
    [SerializeField] [Range(0.01f, 1.0f)] private float smoothing;
    private Vector3 _offset;

    private void Start() {
        _offset = transform.position - playerTransform.position;
    }

    private void LateUpdate() {
        if (Input.GetKey(KeyCode.Mouse1) && shouldRotate) {
            Quaternion rotationAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            _offset = rotationAngle * _offset;

        }
        
        Vector3 newPosition = playerTransform.position + _offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothing);
        transform.LookAt(playerTransform);
    }
}