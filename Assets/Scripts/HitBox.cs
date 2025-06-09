using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level2Player
{
    public class HitBox : MonoBehaviour
    {
        [SerializeField] private LayerMask _enemyLayer;
        void OnTriggerEnter2D(Collider2D col)
        {
            
        }
    }
}