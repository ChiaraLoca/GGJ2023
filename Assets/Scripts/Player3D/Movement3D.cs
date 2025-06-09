using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Level1
{
    public class Movement3D : MonoBehaviour
{
    /*[SerializeField] Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;

    [SerializeField] private float _maxVelocity =10;
    [SerializeField] private bool _isGround;*/
    [SerializeField] private AudioSource _audioSouce;

    public float _speed = 6;
    public float _jumpForce = 6;
    private Rigidbody _rigidbody;
    private Vector2 _input;
    private Vector3 _movementVector;

    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.freezeRotation = true;
    }
    private void Update()
    {
        
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _audioSouce.Play();
        }
    }
    private void FixedUpdate()
    {
        
        _movementVector = _input.x * transform.right * _speed + _input.y * transform.forward * _speed;
        
        _rigidbody.velocity = new Vector3(_movementVector.x, _rigidbody.velocity.y, _movementVector.z);
    }
    private bool IsGrounded()
    {
        
        if (Physics.Raycast(transform.position, Vector3.down, 1.5f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


 
}}
