using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

namespace Level2Player
{
    public class RangedAttack : MonoBehaviour
    {

        [SerializeField] GameObject projectilePrefab;
        [SerializeField] Animator animator;
        [SerializeField] GameObject leaf;

        PlayerController _playerController;
        bool _canShoot;
        // Start is called before the first frame update
        void Start()
        {
            _canShoot = true;
            _playerController = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            
            if (Input.GetKeyDown("j") )
            {
                animator.SetTrigger("attack");

                if (!_canShoot)
                    return;
                leaf.SetActive(false);
                _canShoot = false;
                var proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                proj.GetComponent<Projectile>().Direction = _playerController.m_FacingRight ? Vector2.right : Vector2.left;
                StartCoroutine(WaitAndReload());
            }
        }
        private IEnumerator WaitAndReload()
        {
            yield return new WaitForSeconds(2f);
            _canShoot = true;
            leaf.SetActive(true);
        }
    }

}