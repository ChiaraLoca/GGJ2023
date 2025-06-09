using System;
using System.Collections.Generic;
using UnityEngine;
namespace Level1
{ 
public class Music : MonoBehaviour
{
    [SerializeField] private List<MusicStruct> musicList = new List<MusicStruct>();
    [SerializeField] private AudioSource audioSource;




    [Serializable]
    public struct MusicStruct
    {
        public string name;
        public AudioClip clip;

        public MusicStruct(string name, AudioClip clip)
        {
            this.name = name;
            this.clip = clip;
        }
    }

    internal void SetMusic(string name)
    {
        foreach (MusicStruct at in musicList)
        {
            if (at.name.Equals(name))
            {
                    
                audioSource.clip = at.clip;
                audioSource.Play();
            }
        }
    }
}}