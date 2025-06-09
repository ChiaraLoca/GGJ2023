using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level2Enemy
{

    public class sensoreBasso : MonoBehaviour
    {


        public bool ledge = false;
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


                ledge = false;


            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {

            if (collision.gameObject.layer == LayerMask.NameToLayer("l2Ground"))
            {
                ledge = true;
            }
        }



    }
}
