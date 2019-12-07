using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private string _xMoveInput = "Vertical";
    [SerializeField] private string _zMoveInput = "Horizontal";
    [SerializeField] private string _jumpInput = "Jump";
    [SerializeField] private string _rotationInput = "Mouse X";

    private BasicMovement _characterController;

    private void Start()
    {
        _characterController = GetComponent<BasicMovement>();
    }

    private void Update()
    {
        float xInput = Input.GetAxis(_xMoveInput);
        float zInput = Input.GetAxis(_zMoveInput);
        float yRotation = Input.GetAxis(_rotationInput);
        _characterController.SetMoveInput(xInput, zInput);
      //  transform.Rotate(0f, yRotation, 0f);
        if (Input.GetAxis(_jumpInput) > .1f)
            _characterController.TryJump();

       
    }


}
