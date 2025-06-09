using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Level3
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private SpriteRenderer enemySpriteRenderer;

        [Header("Health")] [SerializeField] private int hp = 1;
        [SerializeField] private Slider slider;
        private int _curHP;


        private void Start()
        {
            _curHP = hp;
            slider.maxValue = hp;
            slider.value = hp;
            StartCoroutine(CycleSprites());
        }


        private IEnumerator CycleSprites()
        {
            while (true)
            {
                yield return new WaitForSeconds(.3f);
                if (sprites.Count >= 1)
                {
                    enemySpriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            ManageCollision(col);
        }

        private void ManageCollision(Collision2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("L3_proj"))
            {
                Hit();
                Destroy(col.gameObject);
            }
            else if (col.gameObject.layer == LayerMask.NameToLayer("L3_Player"))
            {
                col.gameObject.GetComponentInParent<Player>().Hit();
            }
        }

        private void Hit()
        {
            _curHP--;
            if (_curHP <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                slider.value = _curHP;
            }
        }
    }
}