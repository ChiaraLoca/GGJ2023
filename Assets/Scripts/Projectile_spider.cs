using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Level2Enemy
{

    public class Projectile : MonoBehaviour
    {
        private Rigidbody2D rigidbody2d;
        public Vector2 Direction { get; set; }
        [SerializeField] float speed = 10;
        // Start is called before the first frame update
        void Start()
        {
            rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            rigidbody2d.velocity = Direction.normalized * speed;
        }
    }
}
