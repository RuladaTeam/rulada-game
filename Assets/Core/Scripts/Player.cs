using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _speed = 5;
    
    private Rigidbody _rigidbody;
    private PlayerInputActions _playerInputActions;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    private void Update()
    {
        Vector2 direction = _playerInputActions.Player.Move.ReadValue<Vector2>() * _speed;
        _rigidbody.linearVelocity = new Vector3(direction.x, _rigidbody.linearVelocity.y, direction.y);
    }
}
