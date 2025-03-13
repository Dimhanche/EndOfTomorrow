using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Movement

    [SerializeField] private float groundCheckDistance = 0.1f;
    private bool _isGrounded;
    [SerializeField] private Transform _orientation;
    private Vector3 _moveDirection;
    private bool _canMove => _entityInfo.canMove;

    #endregion

    #region Camera Rotation

    [SerializeField][Range(5,100)]private float _baseSensitivity = 10;
    private float _sensitivity;
    private Vector2 _inputRotation = Vector2.zero;
    private Camera _camera;
    private float _xRotation;
    private float _yRotation;
    private float _yRotationLimit = 88f;

    #endregion

    private Vector2 _inputDirection;
    private Rigidbody _rb;
    private EntityInfo _entityInfo;
    private SOStats _stats;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        _entityInfo = GetComponent<EntityInfo>();
        _rb = GetComponent<Rigidbody>();
        _stats = _entityInfo.entity.entityStats;
        _camera = Camera.main;
    }


    void Update()
    {
        CheckGrounded();
        if (_canMove && !_entityInfo.isPaused)
        {
            Movement();
            RotateCamera();
        }
    }


    public void PlayerMovementInput(InputAction.CallbackContext ctx)
    {
        _inputDirection = ctx.ReadValue<Vector2>();
        if (ctx.canceled && _isGrounded)
        {
            _rb.linearVelocity = Vector3.zero;
        }
    }
    public void PlayerJumpInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _isGrounded && _canMove)
        {
            _rb.AddForce(Vector3.up * _stats.jumpForce, ForceMode.Impulse);
        }
    }
    public void PlayerLookInput(InputAction.CallbackContext ctx)
    {
        if(_canMove)
            _inputRotation = ctx.ReadValue<Vector2>();
        else
        {
            _inputRotation = Vector2.zero;
        }
    }

    private void Movement()
    {
        if (!_rb || _inputDirection.Equals(Vector2.zero) && !_isGrounded) return;

        float curSpeedX = _stats.speed * _inputDirection.y;
        float curSpeedY = _stats.speed * _inputDirection.x;

        _moveDirection = _orientation.forward * curSpeedX + _orientation.right * curSpeedY;

        _rb.linearVelocity = _moveDirection + new Vector3(0, _rb.linearVelocity.y, 0);
    }

    private void CheckGrounded()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

    private void RotateCamera()
    {
        _sensitivity = _baseSensitivity;
        float mouseX = _inputRotation.x * _sensitivity * Time.deltaTime;
        float mouseY = _inputRotation.y * _sensitivity * Time.deltaTime;

        _yRotation += mouseX;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -_yRotationLimit, _yRotationLimit);

        _camera.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}
