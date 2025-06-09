using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level1
{
    public class VillageManager : MonoBehaviour
    {


        public void startAudio()
        {
            SubtitlerManager.SetSubtitls("Livello1");
            //ChangeScene.Change("2_Level1");
        }

        public void next()
        {
            Debug.Log("ChangeScene.Change()");
            
            ChangeScene.Change("2_Level1");
        }
    }
}