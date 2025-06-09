using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Level3
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float vSpeed = 1f;
        [SerializeField] private float hSpeed = 1f;
        [SerializeField] private float shootCooldownSecs = .4f;

        private float _lastShootTime = 0f;

        private Coroutine _shootCoroutine;


        [SerializeField] private List<Sprite> playerSprites;
        [SerializeField] private List<Sprite> launchSprites;


        [SerializeField] private Transform endingPlace;


        private Rigidbody2D _rb;
        public Rigidbody2D RB => _rb ??= GetComponentInChildren<Rigidbody2D>();

        private SpriteRenderer _spRenderer;
        public SpriteRenderer SpRenderer => _spRenderer ??= GetComponentInChildren<SpriteRenderer>();

        private KnockBackFromGround _kb;
        public KnockBackFromGround KnockBack => _kb ??= GetComponentInChildren<KnockBackFromGround>();

        private Vector3 startingRot;
        [SerializeField] private GameObject projectilePrefab;
        private Coroutine _byeBye;

        private void OnEnable()
        {
            GameManager.Finish += Finish;
        }

        private void OnDisable()
        {
            GameManager.Finish -= Finish;
        }

        private void Finish(int wait)
        {
            StartCoroutine(FinalPlace(wait));
        }

        private IEnumerator FinalPlace(int wait)
        {
            yield return new WaitForSeconds(wait);
            transform.position = endingPlace.transform.position + Vector3.up * 200;
            KnockBack.transform.localPosition = Vector3.zero;
        }

        private void Start()
        {
            startingRot = SpRenderer.transform.localRotation.eulerAngles;
            StartCoroutine(AlternateSprites());
        }

        private IEnumerator AlternateSprites()
        {
            while (true)
            {
                yield return new WaitForSeconds(.3f);
                yield return _shootCoroutine;
                if (playerSprites.Count >= 1)
                {
                    SpRenderer.sprite = playerSprites[Random.Range(0, playerSprites.Count)];
                }
            }
        }


        void Update()
        {
            _lastShootTime += Time.deltaTime;

            if (Input.GetKey(KeyCode.J) && _lastShootTime > shootCooldownSecs)
            {
                _lastShootTime = 0;
                _shootCoroutine = StartCoroutine(ShootCoroutine());
            }

            if (KnockBack.StopVertical) return;

            transform.position += Vector3.down * (Time.deltaTime * vSpeed);
            SpRenderer.transform.localRotation =
                Quaternion.Euler(startingRot.x, Input.GetAxis("Horizontal") * 60f, startingRot.z);
            if (!KnockBack.KnockingBack)
                RB.velocity = Vector2.right * (Input.GetAxis("Horizontal") * 500f * hSpeed);
        }

        private IEnumerator ShootCoroutine()
        {
            foreach (var sprite in launchSprites)
            {
                SpRenderer.sprite = sprite;
                yield return new WaitForSeconds(shootCooldownSecs / launchSprites.Count);
            }

            var proj = Instantiate(projectilePrefab, SpRenderer.transform.position, Quaternion.identity);


            yield return null;
        }

        public void Hit()
        {
            if (_byeBye != null) return;
            _byeBye = StartCoroutine(ByeByeCoroutine());
        }

        private IEnumerator ByeByeCoroutine()
        {
            for (int i = 0; i < 5; i++)
            {
                SpRenderer.enabled = !SpRenderer.enabled;
                yield return new WaitForSeconds(.2f);
            }

            GameOverUtility.GameOver();
        }
    }
}