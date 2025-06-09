using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level2Enemy
{

    public class Spider : MonoBehaviour
    {
        [SerializeField] GameObject projectile;
        public bool canFire = true;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //fire(PlayerPosition.playerTransform.transform);
        }

        private void fire(Transform target)
        {
            if (canFire)
            {

                canFire = false;
                Vector3 distance = target.position - gameObject.transform.position;
                Vector2 direction = new Vector2(distance.x, distance.y);
                fireProjectile(direction);
                StartCoroutine(WaitAndReload());
            }
        }

        private IEnumerator WaitAndReload()
        {
            yield return new WaitForSeconds(3f);
            canFire = true;
        }

        private void fireProjectile(Vector2 direction)
        {
            Projectile projectile_instance = Instantiate(projectile, transform.position, transform.rotation).GetComponent<Projectile>();
            projectile_instance.Direction = direction;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("l2Projectile"))
            {
                    Destroy(gameObject);
            }
        }
    }
}
