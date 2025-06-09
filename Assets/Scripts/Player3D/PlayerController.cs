using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Level1
{
    public class PlayerController : MonoBehaviour
{
    Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    

    internal void Die()
    {
        Debug.Log("MORTO");
        transform.position = _startPosition;
    }
}}
