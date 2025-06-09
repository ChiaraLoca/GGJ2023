using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Level1
{
    public class KillCondition3D : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private AudioSource _audioSouce;
    [SerializeField] private AudioClip _dieClip;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("KillTerrain"))
        {
            _audioSouce.PlayOneShot(_dieClip);
            _playerController.Die();

        }
    }
}}
