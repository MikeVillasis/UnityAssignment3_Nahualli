using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSimpleMovement : MonoBehaviour
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private Transform _cameraOffset;
    [SerializeField] private float _xSensitivity = 80f;
    [SerializeField] private CameraChanger _cameraChanger;


    private float _headingCamera = 5f;

    private void Start()
    {        
        _cameraPivot.transform.position = _targetPosition.position;
        _cameraPivot.transform.parent = _targetPosition.transform;
        transform.parent = _cameraOffset.transform;
        
    }
    private void Update()
    {
        if (!_cameraChanger._IsCamera2D)
            FollowPlayerIn3D();
    }

    private void FollowPlayerIn3D()
    {
        _headingCamera += Input.GetAxis("Mouse X") * Time.deltaTime * 45f;
        _cameraPivot.rotation = Quaternion.Euler(0, _headingCamera, 0);
        // float cameraYAnlge = _targetPosition.eulerAngles.y;
        transform.rotation = _cameraPivot.rotation;
        transform.LookAt(_targetPosition);
    }
}
