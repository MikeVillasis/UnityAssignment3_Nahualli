using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private string _xMoveInput = "Vertical";
    [SerializeField] private string _zMoveInput = "Horizontal";
    [SerializeField] private string _jumpInput = "Jump";


    private BasicMovement _characterController;

    private void Start()
    {
        _characterController = GetComponent<BasicMovement>();
    }

    private void Update()
    {
        float xInput = Input.GetAxis(_xMoveInput);
        float zInput = Input.GetAxis(_zMoveInput);
        _characterController.SetMoveInput(xInput, zInput);

        if (Input.GetAxis(_jumpInput) > .1f)
            _characterController.TryJump();

    }


}
