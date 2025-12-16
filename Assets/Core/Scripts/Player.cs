using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private int _speed = 5;
    [SerializeField] private int _jumpForce = 10;
    [SerializeField] private int _coinsCollected = 0;
    
    private Rigidbody _rigidbody;
    private PlayerInputActions _playerInputActions;

    private bool _canJump = false;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Jump.performed += Jump_OnPerformed;
        
        _playerInputActions.Enable();
    }

    private void Jump_OnPerformed(InputAction.CallbackContext obj)
    {
        if (_canJump)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce); // new Vector3(0, 1, 0);
            _canJump = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            _canJump = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Collectable"))
        {
            _coinsCollected++;
            Destroy(other.gameObject);
        }
    }

    // устанавливать переменную _canJump в false можно при прекращении коллизии с полом, а не при каждом прыжке
    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
    //     {
    //         _canJump = false;
    //     }
    // }

    private void Update()
    {
        Vector2 direction = _playerInputActions.Player.Move.ReadValue<Vector2>() * _speed;
        _rigidbody.linearVelocity = new Vector3(direction.x, _rigidbody.linearVelocity.y, direction.y);
    }
}
