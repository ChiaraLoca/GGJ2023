using System;
using System.Collections;
using UnityEngine;

namespace Level3
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int gameDurationSecs = 60;
        [SerializeField] private int waitBeforeShowingFinal = 12;


        public delegate void GameFinished(int extraWaiting);

        public static event GameFinished Finish;


        private void Start()
        {
            StartCoroutine(RunGame());
        }

        private IEnumerator RunGame()
        {
            yield return new WaitForSeconds(gameDurationSecs);
            Finish?.Invoke(waitBeforeShowingFinal);
        }
    }
}