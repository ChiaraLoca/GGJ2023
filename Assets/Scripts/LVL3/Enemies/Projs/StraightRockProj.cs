using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Level3.Projs
{
    public class StraightRockProj : MonoBehaviour
    {
        [SerializeField] private float speed = 20;


        private void Update()
        {
            StartCoroutine(RotateSelf());
            transform.position += Vector3.up * (speed * Time.deltaTime);
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
            Destroy(this);
        }
    }
}