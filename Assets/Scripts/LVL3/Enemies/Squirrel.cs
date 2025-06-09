using System;
using System.Collections;
using UnityEngine;

namespace Level3
{
    [RequireComponent(typeof(Enemy))]
    public class Squirrel : MonoBehaviour
    {
        private Enemy _enmy;
        private Enemy Enmy => _enmy ??= GetComponent<Enemy>();
        [SerializeField] private GameObject projprefab;


        private void Start()
        {
            StartCoroutine(SquirrelCoroutine());
        }

        private IEnumerator SquirrelCoroutine()
        {
            yield return new WaitForSeconds(5f);
            Instantiate(projprefab, transform.position, Quaternion.identity);
            GetComponent<Rigidbody2D>().velocity = Vector2.down * 200;
        }
    }
}