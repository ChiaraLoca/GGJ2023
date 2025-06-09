using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level3
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Wall : MonoBehaviour
    {
        private SpriteRenderer _spRender;
        public SpriteRenderer SpRenderer => _spRender ??= GetComponent<SpriteRenderer>();

        [SerializeField] private List<Material> possibleMats;
        [SerializeField] private List<Material> woodMats;
        [SerializeField] private MeshRenderer upperImage;

        public bool IsCentral { private get; set; } = false;


        public bool IsVisible => SpRenderer.isVisible;

        private void Start()
        {
            if (IsCentral)
            {
                if (woodMats.Count > 0)
                {
                    upperImage.material = woodMats[Random.Range(0, woodMats.Count)];
                    upperImage.sortingOrder = 2;
                }
            }
            else
            {
                if (possibleMats.Count > 0)
                {
                    upperImage.material = possibleMats[Random.Range(0, possibleMats.Count)];
                }
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}