using UnityEngine;

public class MovementController : MonoBehaviour {
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    private Vector3 _direction;
    private float _speed;
    
    private void Update() {
        _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        _speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        transform.Translate(_direction * _speed * Time.deltaTime);
        
        _direction = Vector3.zero;
        Debug.Log("Horiz: " + Input.GetAxis("Horizontal") + " .. Vertic: " + Input.GetAxis("Vertical"));
    }
}   