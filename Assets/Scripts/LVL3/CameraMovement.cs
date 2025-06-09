using System;
using System.Collections;
using Level3;
using UnityEngine;
using UnityEngine.Rendering;

namespace Level3
{
    [RequireComponent(typeof(Camera))]
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private ObstacleGenerator obstacleGenerator;

        [SerializeField] private float updateCycleDurationSecs = 1f;
        [SerializeField] private Transform endingPlace;

        private bool _stop = false;


        private float _startingX;

        private float yOffset;
        private float _timer;
        private Coroutine _moveCoroutine;

        private void Start()
        {
            var position = target.position;
            var position1 = transform.position;
            yOffset = position1.y - position.y;
            position1 = new Vector3(position.x, position.y + yOffset, position1.z);
            transform.position = position1;
        }

        void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > updateCycleDurationSecs && !_stop)
            {
                _timer = 0;
                _moveCoroutine =
                    StartCoroutine(InterpolateCamera(obstacleGenerator.OffsetX, target.position.y + yOffset));
            }
        }

        private void OnEnable()
        {
            GameManager.Finish += Ending;
        }

        private void OnDisable()
        {
            GameManager.Finish -= Ending;
        }

        private void Ending(int wait)
        {
            StartCoroutine(EndingCoroutine(wait));
        }

        private IEnumerator EndingCoroutine(int wait)
        {
            yield return new WaitForSeconds(wait);
            _stop = true;
            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);
            yield return new WaitForEndOfFrame();
            transform.position = new Vector3(endingPlace.position.x, endingPlace.position.y, -500f);
        }

        private IEnumerator InterpolateCamera(float obstacleGeneratorOffsetX, float positionY)
        {
            Vector3 startingPos = transform.position;
            float progress = 0f;
            while (progress <= 1f)
            {
                transform.position = Vector3.Lerp(
                    startingPos,
                    new Vector3(_startingX + obstacleGeneratorOffsetX, positionY, startingPos.z),
                    progress
                );
                progress += Time.deltaTime / updateCycleDurationSecs;
                yield return new WaitForEndOfFrame();
            }

            yield return null;
        }
    }
}