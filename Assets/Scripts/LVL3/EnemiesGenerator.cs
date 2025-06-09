using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Level3
{
    public class EnemiesGenerator : MonoBehaviour
    {
        [SerializeField] private ObstacleGenerator obstacleGenerator;

        [SerializeField] private List<GameObject> enemiesPrefabs;
        private Coroutine _spawnCoroutine;

        void Start()
        {
            _spawnCoroutine = StartCoroutine(SpawnEnemies());
        }

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

        private IEnumerator SpawnEnemies()
        {
            float range = obstacleGenerator.TunnelWidth / 2f - 100f;
            while (true)
            {
                if (enemiesPrefabs.Count < 1) yield break;
                yield return new WaitUntil(() => !obstacleGenerator.SplitSection);
                Instantiate(
                    enemiesPrefabs[Random.Range(0, enemiesPrefabs.Count)],
                    transform.position +
                    Vector3.right * (obstacleGenerator.OffsetX + Random.Range(-range, range)) +
                    Vector3.up * obstacleGenerator.Offset2D.y,
                    Quaternion.identity
                );
                yield return new WaitForSeconds(Random.Range(3f, 10f));
            }
        }
    }
}