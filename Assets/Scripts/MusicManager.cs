using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Level1
{
    public class MusicManager : MonoBehaviour
{
    private static Music music;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        music = GetComponentInChildren<Music>();
    }
    public static void SetMusic(string name)
    {
        music.SetMusic(name);
    }
}}
