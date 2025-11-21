using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float jumnpPower = 3;
    private CharacterController _characterController;
    private Transform _transform;
    private Vector3 _monoVelocity;

    private void Start()
    {
        _characterController = GetComponent<CharacterController> ();
        _transform = transform;
    }

    private void Update()
    {

    }
}
