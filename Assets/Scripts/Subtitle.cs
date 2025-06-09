using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Level1
{
    public class Subtitle : MonoBehaviour
{
    [SerializeField] private List<AudioText> audioTextList = new List<AudioText>();
    [SerializeField] private TextMeshProUGUI textArea; 
    [SerializeField] private AudioSource audioSource;

    internal void SetSubtitls(string name)
    {
        foreach(AudioText at in audioTextList)
        {
            if(at.name.Equals(name))
            {
                textArea.text = at.text;
                audioSource.clip = at.clip;
                audioSource.Play();
            }
        }
    }

    [Serializable]
    public struct AudioText
    {
        public string name;
        public string text;
        public AudioClip clip;

        public AudioText(string name,string text, AudioClip clip)
        {
            this.name = name;
            this.text = text;
            this.clip = clip;
        }
    }
}}
