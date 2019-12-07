
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class BasicMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private bool _canWalkOffLedges = false;
    [SerializeField] private Transform _gameCamera;

    [Header("Jump")]
    [SerializeField] private float _jumpVelocity = 5f;
    [SerializeField] private float _airControl = 0.1f;

    [Header("Grounding")]
    [SerializeField] private float _groundCheckDistance = 0.25f;
    [SerializeField] private float _groundCheckOffset = .35f;
    [SerializeField] private LayerMask _groundCheckMask;

    private Rigidbody _rigidBody;
    private Vector3 _camForward;
    private Vector3 _camRight;
    private float _xAxis;
    private float _zAxis;

    public bool IsGrounded { get; private set; }
    public bool IsTryingToMove { get; private set; }

    private float[] _userMoveInput = new float[2];

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        
    }
    public void SetMoveInput(float xInput, float zInput)
    {
        //if(!_canWalkOffLedges)
        //{
        //    if (input > 0f && !GroundCheck(_groundCheckOffset) || input < 0f && !GroundCheck(-_groundCheckOffset))
        //        input = 0f;
        //}
        _userMoveInput[0] = xInput;
        _userMoveInput[1] = zInput;
        IsTryingToMove = Mathf.Abs(_userMoveInput[0]) >= 0.05f || Mathf.Abs(_userMoveInput[1]) >= 0.05f;

    }

    public void TryJump()
    {
        if (!IsGrounded) return;

        Vector3 currentVelocity = _rigidBody.velocity;
        currentVelocity.y = _jumpVelocity;
        _rigidBody.velocity = currentVelocity;
    }
    private void Update()
    {
        _camForward = _gameCamera.forward;
        _camRight = _gameCamera.right;
        _camForward.y = 0;
        _camRight.y = 0;
        _camForward = _camForward.normalized;
        _camRight = _camRight.normalized;
        bool grounded = GroundCheck(-_groundCheckOffset) || GroundCheck(_groundCheckOffset);
        IsGrounded = grounded;
        //_userMoveInput = new Vector3(_xAxis * _camForward.x, 0f, _zAxis * _camRight.z);

        //if (Input.GetButtonDown("Jump"))
        //    _rigidBody.AddForce(new Vector3(0f, _jumpForce * _movementSpeed, 0f));
    }

    private void FixedUpdate()
    {
        float control = 0f;
        if (IsGrounded) control = 1f;
        else if(IsTryingToMove) control = _airControl;
        float xTargetVelocity = _userMoveInput[0] * _speed;
        float zTargetVelocity = _userMoveInput[1] * _speed;
        float xCurrentVelocity = _rigidBody.velocity.x;
        float zCurrentVelocity = _rigidBody.velocity.z;
        float xVelocityDifference = xTargetVelocity - xCurrentVelocity;
        float zVelocityDifference = zTargetVelocity - zCurrentVelocity;
        float xMoveForce = xVelocityDifference * _acceleration * control;
        float zMoveForce = zVelocityDifference * _acceleration * control;
        xMoveForce = xMoveForce * _camForward.x;
        zMoveForce = zMoveForce * _camRight.z;
        _rigidBody.AddForce(new Vector3(xMoveForce, 0f, zMoveForce));
    }

    private bool GroundCheck(float horizontalOffset)
    {
        Vector3 offset = new Vector3(horizontalOffset, _groundCheckDistance * 0.5f);
        Vector3 origin = offset + transform.position;
        if (Physics.Raycast(origin, Vector3.down, _groundCheckDistance, _groundCheckMask))
        {
            Debug.DrawRay(origin, Vector3.down * _groundCheckDistance, Color.cyan);
            return true;
        }
        else
        {
            Debug.DrawRay(origin, Vector3.down * _groundCheckDistance, Color.red);
            return true;
        }
    }
}
