using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level2Enemy
{
    public class Ant : MonoBehaviour
    {
        [SerializeField] float walk_speed = 1f;
        [SerializeField] float acceleration = 5f;
        [SerializeField] float knockback_x_acceleration = 30f;
        [SerializeField] float knockback_y_acceleration = 200f;
        [SerializeField] Animator animator;
        [SerializeField] GameObject sprite;
        bool _health;
        public bool grounded = false;
        public bool canBeKnocked = true;
        public bool knocked = false;
        [SerializeField] sensoreAlto s_alto;
        [SerializeField] sensoreBasso s_basso;
        [SerializeField] GameObject sensori;
        public int direction = 1;
        Rigidbody2D rigidbody2d;
        // Start is called before the first frame update
        void Start()
        {
            _health = true;
            rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            walk();


        }

        private void walk()
        {
            if (knocked)
            {
                return;
            }
            if (grounded && (s_alto.wall || s_basso.ledge))
            {
                flip();
            }
            if (grounded)
            {
                move();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("l2Projectile"))
            {
                Debug.Log("hit baby");
                if (_health)
                    knockback(collision);
                else
                    Destroy(gameObject);
            }
            
            if (collision.gameObject.layer == LayerMask.NameToLayer("l2Ground"))
            {
                grounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("l2Ground"))
            {
                grounded = false;
            }
        }

        private void flip()
        {

            direction *= -1;

            sensori.gameObject.SetActive(false);
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, 1, 1);
            s_alto.wall = false;
            s_basso.ledge = false;
            //sensori.transform.rotation = Quaternion.Euler(new Vector3(0, direction < 0 ? 180f : 0, 0));

            sensori.gameObject.SetActive(true);


            rigidbody2d.velocity = -rigidbody2d.velocity;

        }

        private void move()
        {

            rigidbody2d.AddForce(new Vector2(acceleration * direction, 0));
            if (Mathf.Abs(rigidbody2d.velocity.x) > walk_speed)
            {
                rigidbody2d.velocity = new Vector2(walk_speed * direction, rigidbody2d.velocity.y);
            }


        }

        private void knockback(Collision2D collision)
        {
            if (canBeKnocked)
            {
                _health = false;
                canBeKnocked = false;
                knocked = true;
                animator.SetBool("knockback", knocked);
                Vector3 distance = collision.gameObject.transform.position - gameObject.transform.position;
                Vector2 distance2 = new Vector2(distance.x, distance.y);
                rigidbody2d.AddForce(new Vector2(-distance2.x * knockback_x_acceleration, knockback_y_acceleration));
                StartCoroutine(WaitAndReload());
            }
        }

        private IEnumerator WaitAndReload()
        {
            yield return new WaitForSeconds(1f);
            canBeKnocked = true;
            knocked = false;
            animator.SetBool("knockback", knocked);
        }

    }
}