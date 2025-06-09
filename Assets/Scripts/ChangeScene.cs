using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Level1
{

    public class ChangeScene : MonoBehaviour
    {
        private void Awake()
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
            MusicManager.SetMusic("0_Menu");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
                Change("0_Menu");
            if (Input.GetKeyDown(KeyCode.Alpha1))
                Change("1_Village");
            if (Input.GetKeyDown(KeyCode.Alpha2))
                Change("2_Level1");
            if (Input.GetKeyDown(KeyCode.Alpha3))
                Change("3_Level2");
            if (Input.GetKeyDown(KeyCode.Alpha4))
                Change("level3");
            if (Input.GetKeyDown(KeyCode.Alpha5))
                Change("Terminal-test");

        }

        public static void Change(string name)
        {
            SceneManager.LoadScene(name, LoadSceneMode.Single);

            MusicManager.SetMusic(name);
        }
    }

}