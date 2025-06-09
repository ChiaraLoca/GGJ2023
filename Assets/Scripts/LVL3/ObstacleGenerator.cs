using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level3
{
    public enum LRDirection
    {
        Left,
        Right,
        Center
    }

    public class ObstacleGenerator : MonoBehaviour
    {
        private const float kStartingSplitWidthReduction = 20f;
        [SerializeField] private float tunnelWidth = 400f;
        [SerializeField] private float maxDispersion = 5f;

        [SerializeField] private float splitIntervalSecs = 30f;

        [SerializeField] private Transform playerTransform;

        private float _splitReduction = kStartingSplitWidthReduction;

        private bool _canSplit = false;
        public bool SplitSection => _canSplit;

        private Wall _firstWall = null;

        [SerializeField] private GameObject wallPrefab;

        private Vector2 _objSize = Vector2.zero;
        private Vector2 _curPos = Vector2.zero;
        private float _timer;
        private Coroutine _spawnCoroutine;

        public float OffsetX => _curPos.x;
        public Vector2 Offset2D => _curPos;
        public float TunnelWidth => tunnelWidth;


        private void OnEnable()
        {
            GameManager.Finish += StopIt;
        }

        private void OnDisable()
        {
            GameManager.Finish -= StopIt;
        }

        private void StopIt(int wait)
        {
            if (_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);
        }


        private void Start()
        {
            BoxCollider2D boxCollider2D = wallPrefab.GetComponentInChildren<BoxCollider2D>();
            _objSize = new Vector2(
                boxCollider2D.size.x * boxCollider2D.transform.localScale.x,
                boxCollider2D.size.y * boxCollider2D.transform.localScale.y
            );
            _spawnCoroutine = StartCoroutine(CycleSpawn());
        }

        private IEnumerator CycleSpawn()
        {
            while (true)
            {
                if (_firstWall != null && playerTransform.position.y < _firstWall.transform.position.y)
                {
                    Vector2 baseVec = playerTransform.position.x < _firstWall.transform.position.x
                        ? Vector2.left
                        : Vector2.right;

                    _curPos += baseVec * tunnelWidth / 2f;

                    _firstWall = null;
                    _canSplit = false;
                    _splitReduction = kStartingSplitWidthReduction;
                }

                Wall rWall = GenerateWall(LRDirection.Right);
                Wall lWall = GenerateWall(LRDirection.Left);
                _curPos += new Vector2(Random.Range(-maxDispersion, maxDispersion), -_objSize.y);
                if (_canSplit)
                {
                    Wall wall = GenerateWall(LRDirection.Center);
                    _firstWall ??= wall;
                }

                yield return new WaitUntil(() => lWall.IsVisible && rWall.IsVisible);
            }
        }


        private Wall GenerateWall(LRDirection direction)
        {
            Vector2 dirVector = direction switch
            {
                LRDirection.Left => Vector2.left,
                LRDirection.Right => Vector2.right,
                LRDirection.Center => Vector2.zero,
                _ => Vector3.right
            };
            Vector3 pos = _curPos + dirVector * (tunnelWidth / 2f + _objSize.x / 2f);
            GameObject wall = Instantiate(
                wallPrefab,
                pos,
                Quaternion.identity,
                transform
            );
            Wall wallCpt = wall.GetComponentInChildren<Wall>();

            if (direction == LRDirection.Center)
            {
                wallCpt.IsCentral = true;
                Vector3 scale = wall.transform.localScale;
                scale.x /= _splitReduction;
                _splitReduction = Mathf.Clamp(_splitReduction - 2, 5f, kStartingSplitWidthReduction);
                wall.transform.localScale = scale;
            }

            wall.name = direction switch
            {
                LRDirection.Left => "LWall",
                LRDirection.Right => "RWall",
                _ => "Wall"
            };
            return wallCpt;
        }


        // Update is called once per frame
        void Update()
        {
            if (!_canSplit)
            {
                _timer += Time.deltaTime;
            }

            if (_timer > splitIntervalSecs)
            {
                _canSplit = true;
                _timer = 0f;
            }
        }
    }
}