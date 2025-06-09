using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3
{
    public class KnockBackFromGround : MonoBehaviour
    {
        private bool _knockingBack = false;
        public bool KnockingBack => _knockingBack;

        private Rigidbody2D _rb;
        public Rigidbody2D RB => _rb ??= GetComponent<Rigidbody2D>();

        private bool _stopVertical = false;
        public bool StopVertical => _stopVertical;

        public delegate void PlayerAction();

        public static event PlayerAction UnlockTerminal;

        private void OnCollisionEnter2D(Collision2D col)
        {
            KnockBack(100f, col.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("L3_terminal"))
            {
                UnlockTerminal?.Invoke();
            }
        }


        private void KnockBack(float velocityModule, GameObject col)
        {
            if (col.CompareTag("EndingPlatform"))
            {
                _stopVertical = true;
                return;
            }

            if (col.layer != LayerMask.NameToLayer("L3_ground")) return;

            _knockingBack = true;
            StartCoroutine(UpdateKnockingBack());
            Vector3 backDir = transform.position.x < col.transform.position.x ? Vector3.left : Vector3.right;
            RB.velocity = backDir * velocityModule;
        }

        private IEnumerator UpdateKnockingBack()
        {
            yield return new WaitForSeconds(.5f);
            _knockingBack = false;
        }
    }
}