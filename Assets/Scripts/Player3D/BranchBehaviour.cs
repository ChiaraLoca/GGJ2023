using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Level1
{
    public class BranchBehaviour : MonoBehaviour
{
    [SerializeField] private bool _isStable;

    private bool _firstCollision =true;

    private void OnCollisionEnter(Collision collision)
    {
        if(!_isStable)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                if (_firstCollision)
                {
                    _firstCollision = false;

                    float f = Random.Range(0.2f, 5f);
                    StartCoroutine(WaitAndFall(f));

                }
            }
        }
    }

    private IEnumerator WaitAndFall(float time)
    {


        yield return new WaitForSeconds(time);

       

        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().AddForce(0,-5,0);
    }
}}
