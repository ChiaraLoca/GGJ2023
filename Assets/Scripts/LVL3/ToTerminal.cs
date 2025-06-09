using System;
using System.Collections;
using System.Collections.Generic;
using Level3;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level3
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ToTerminal : MonoBehaviour
    {
        private TextMeshProUGUI _txt;
        public TextMeshProUGUI Txt => _txt ??= GetComponent<TextMeshProUGUI>();

        private bool _isUnlocked = false;
        
        private void OnEnable()
        {
            KnockBackFromGround.UnlockTerminal += UnlockTerminal;
        }

        private void OnDisable()
        {
            KnockBackFromGround.UnlockTerminal -= UnlockTerminal;
        }

        private void UnlockTerminal()
        {
            Txt.SetText("Premi E per aprire il terminale");
            _isUnlocked = true;
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && _isUnlocked)
            {
                SceneManager.LoadScene("Terminal-test");
            }
        }
    }
}