using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel2ndLevel : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndPlay());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -50)
        {
            Destroy(gameObject);
        }   
    }


    void OnBecameVisible()
    {
        Start();
    }
    public IEnumerator WaitAndPlay()
    {
        yield return new WaitForSeconds(3);
        audioSource.Play();
    }
}
