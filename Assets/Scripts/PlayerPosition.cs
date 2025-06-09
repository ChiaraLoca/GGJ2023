using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Level2Enemy
{

    public class PlayerPosition : MonoBehaviour
    {
        public static GameObject playerTransform;

        private void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
