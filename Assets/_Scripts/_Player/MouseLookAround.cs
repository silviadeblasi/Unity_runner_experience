using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookAround : MonoBehaviour
{
    private float _rotationX = 0f;
    private float _rotationY = 0f;

    [SerializeField] private float _mouseSensitivity = 15f;

    [SerializeField] private Transform _playerBody;
    [SerializeField] private Vector3 _offset; 


    void Update()
    {

        //follow player
        transform.position = _playerBody.position + _offset;

        //rotation mouse
        _rotationX += Input.GetAxis("Mouse X") * _mouseSensitivity;
        _rotationY += Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);

        transform.localRotation = Quaternion.Euler(-_rotationY, _rotationX, 0f);
        
    }
}
