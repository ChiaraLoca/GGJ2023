using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level2Enemy
{
    public class hopper : MonoBehaviour
    {
        [SerializeField] float walk_speed = 1f;
        [SerializeField] float acceleration = 5f;
        [SerializeField] float knockback_x_acceleration = 30f;
        [SerializeField] float knockback_y_acceleration = 200f;
        [SerializeField] float jumpForce_x = 400f;
        [SerializeField] float jumpForce_y = 400f;
        [SerializeField] Animator animator;
        bool _health;
        public bool grounded = false;
        public bool canBeKnocked = true;
        public bool knocked = false;
        public bool jumping = false;
        public bool canJump = true;
        [SerializeField] sensoreAlto s_alto;
        [SerializeField] sensoreBasso s_basso;
        [SerializeField] GameObject sensori;
        public int direction = 1;
        Rigidbody2D rigidbody2d;
        SpriteRenderer sprite;
        // Start is called before the first frame update
        void Start()
        {
            _health = false;
            rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
            sprite = gameObject.GetComponent<SpriteRenderer>();
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
                jump();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("l2Projectile"))
            {
                if (_health)
                    knockback(collision);
                else
                    Destroy(gameObject);
            }
            
            if (collision.gameObject.layer == LayerMask.NameToLayer("l2Ground"))
            {
                animator.SetTrigger("ground");
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

            sprite.flipX = !sprite.flipX;

        }

        private void jump()
        {
            if (canJump)
            {
                canJump = false;
                jumping = true;
                animator.SetTrigger("jump");
                Vector2 jumpDirection = new Vector2(direction * jumpForce_x, jumpForce_y);
                rigidbody2d.AddForce(jumpDirection);
                StartCoroutine(WaitForJump());
            }



        }

        private void knockback(Collision2D collision)
        {
            if (canBeKnocked)
            {
                _health = false;
                canBeKnocked = false;
                knocked = true;
                animator.SetTrigger("knockback");
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
        }

        private IEnumerator WaitForJump()
        {
            yield return new WaitForSeconds(3f);
            canJump = true;
            jumping = false;
        }
    }
}
