using System.Collections;
using System.Collections.Generic;
using Level2Enemy;
using UnityEngine;
using Level1;

public class followPlayer : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;

    float startX = -12.5f;
    float startZ = -60f;
    bool startPos;
    float value = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        SubtitlerManager.SetSubtitls("Livello2-2");
        startPos = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPos)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(startX, _playerPosition.position.y, startZ), value += (Time.deltaTime / 50));

            if (value >= 1f)
            {
                startPos = true;
            }
        }
        else
        {
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector3(position.x, _playerPosition.position.y > -105 ? _playerPosition.position.y : -105, position.z);
            transform1.position = position;
        }
    }
}
