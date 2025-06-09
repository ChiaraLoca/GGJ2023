using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level2Enemy
{
    public class sensoreAlto : MonoBehaviour
    {

        public bool wall = false;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("l2Ground"))
            {
                //Debug.Log("enter" + collision.gameObject.name);

                wall = true;


            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //Debug.Log("exit" + collision.gameObject.name);
            if (collision.gameObject.layer == LayerMask.NameToLayer("l2Ground"))
            {
                wall = false;
            }
        }


    }
}
