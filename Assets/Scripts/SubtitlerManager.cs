using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Level1
{
    public class SubtitlerManager : MonoBehaviour
{
    private static Subtitle Subtitle;

    void Start()
    {
            GameObject.DontDestroyOnLoad(this.gameObject);
            Subtitle = GetComponentInChildren<Subtitle>();
    }

        public static void SetSubtitls(string name)
    {
        Subtitle.SetSubtitls(name);
    }

}}
