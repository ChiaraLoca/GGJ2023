using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Level1
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private bool _canOpenDoor = true;
        private bool _firstOpen = true;






        private IEnumerator OpenDoor(Vector3 start, Vector3 end)
        {
            
            yield return null;
            animator.SetTrigger("Audio");
            SubtitlerManager.SetSubtitls("Livello2-1");
            _firstOpen = false;

            float elpsedTime = 0;
            while (elpsedTime < 2)
            {
                transform.localPosition = Vector3.Lerp(start, end, (elpsedTime / 2));
                elpsedTime += Time.deltaTime;
                yield return null;
            }

            //ChangeScene.Change("3_Level2");

            //Destroy(gameObject, 2);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(_firstOpen)
            {
                Debug.Log("OnCollisionEnter: " + collision.gameObject.tag);
                if (_canOpenDoor && collision.gameObject.tag.Equals("Player"))
                {
                    Debug.Log("OnCollisionEnter: " + collision.gameObject.tag);
                    _canOpenDoor = false;
                    StartCoroutine(OpenDoor(transform.position, transform.position + new Vector3(0, -12, 0)));
                }
            }
        }

        public void next()
        {
            ChangeScene.Change("3_Level2");
        }
}}


