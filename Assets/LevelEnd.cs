using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Level1;

public class LevelEnd : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.gameObject.tag.Equals("Player"))
            ChangeScene.Change("level3");
    }
}
