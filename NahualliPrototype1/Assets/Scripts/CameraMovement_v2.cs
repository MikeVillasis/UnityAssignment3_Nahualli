using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraMovement_v2 : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private Transform _playerController;
    [SerializeField] private float _rotationSpeed = 15f;
    [SerializeField] private float _lerpSpeed = 5f;

    private float _mouseRotation = 0f;

    private void Update()
    {
        _mouseRotation += Input.GetAxis("Mouse X") * Time.deltaTime * _rotationSpeed;
        //_playerController.rotation = Quaternion.Euler(0f, _mouseRotation, 0f);
        transform.position = Vector3.Lerp(transform.position, _followTarget.position, _lerpSpeed * Time.deltaTime);
    }
}
