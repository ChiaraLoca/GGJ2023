using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;


namespace Level3
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Sprite specialSprite;
        [SerializeField] private int specialRarity = 1000;

        private bool _specialProj = false;

        private void Start()
        {
            _specialProj = Random.Range(0f, specialRarity) < 1f;
            if (_specialProj)
            {
                GetComponentInChildren<SpriteRenderer>().sprite = specialSprite;
            }

            StartCoroutine(RotateSelf());
        }

        private void Update()
        {
            transform.localPosition += Vector3.down * (Time.deltaTime * 250);
        }

        private IEnumerator RotateSelf()
        {
            float rotationSpeed = Random.Range(-1000f, 1000f);
            while (true)
            {
                transform.localRotation = Quaternion.Euler(0, 0,
                    transform.localRotation.eulerAngles.z + rotationSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameObject coll = collision.gameObject;
            if (coll.layer == LayerMask.NameToLayer("L3_Enemy"))
            {
                Destroy(coll);
            }
        }
    }
}