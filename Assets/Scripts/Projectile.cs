using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Level2Player
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] float projectileSpeed = 1f;

        bool _alive;
        public Vector2 Direction { get; set; }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Direction * projectileSpeed);
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if(col.collider.tag!="Player")
                Destroy(gameObject);
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
