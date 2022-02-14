using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    [SerializeField] [Range(0.01f, 1.0f)] private float smoothing;
    private Vector3 _offset;

    private void Start() {
        _offset = transform.position - playerTransform.position;
    }

    private void LateUpdate() {
        Vector3 newPosition = playerTransform.position + _offset;
        
        transform.position = (Vector3.Lerp(transform.position, newPosition, smoothing));
        transform.LookAt(playerTransform);
    }
}