using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Level1
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Button _startButton;
        [SerializeField] private GameObject _fadeOutPanel;


        private void Start()
        {
            _startButton.onClick.AddListener(() => { 
                _fadeOutPanel.SetActive(true); 
                _animator.SetTrigger("Fade");
                SubtitlerManager.SetSubtitls("Intro");
            });

        }

        public void next()
        {
            ChangeScene.Change("1_Village");
        }
    }
    

}
