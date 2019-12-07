using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private GameObject _playerCharacter;
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private Transform _cameraDestination;
    [SerializeField] private float _cameraTime;

    public bool _IsCamera2D { get; private set; }
    private float _distance = 0f;
    private bool _IsPlayerHit = false;

    private void OnTriggerEnter(Collider other)
    {
        _distance = Vector3.Distance(_cameraPosition.position, _cameraDestination.position);
        if (other.name == _playerCharacter.name && !_IsCamera2D && _distance > 0.5f)
        {
            _cameraPosition.position = Vector3.Slerp(_cameraPosition.position, _cameraDestination.position, _cameraTime * Time.deltaTime);
            _IsPlayerHit = true;
            Debug.Log("Hit player");
        }
    }

    private void Update()
    {
        if (_IsPlayerHit && !_IsCamera2D)
        {
            MoveCamera();
        }
    }
    private void MoveCamera()
    {
        _cameraPosition.position = Vector3.Slerp(_cameraPosition.position, _cameraDestination.position, _cameraTime * Time.deltaTime);
        _distance = Vector3.Distance(_cameraPosition.position, _cameraDestination.position);
        Debug.Log(_distance);
        if(_distance < 0.5f)
            _IsCamera2D = true;       
        
    }
}
