using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private int _speed = 5;
    [SerializeField] private int _jumpForce = 10;
    
    private Rigidbody _rigidbody;
    private PlayerInputActions _playerInputActions;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Jump.performed += Jump_OnPerformed;
        
        _playerInputActions.Enable();
    }

    private void Jump_OnPerformed(InputAction.CallbackContext obj)
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce); // new Vector3(0, 1, 0);
    }

    private void Update()
    {
        Vector2 direction = _playerInputActions.Player.Move.ReadValue<Vector2>() * _speed;
        _rigidbody.linearVelocity = new Vector3(direction.x, _rigidbody.linearVelocity.y, direction.y);
    }
}
