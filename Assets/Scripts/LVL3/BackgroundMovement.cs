using UnityEngine;

namespace Level3
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BackgroundMovement : MonoBehaviour
    {
        [SerializeField] private float scrollingSpeed = .1f;


        private MeshRenderer _backGround;
        private MeshRenderer BackGround => _backGround ??= GetComponent<MeshRenderer>();

        private Material BackgroundMaterial => BackGround.material;


        // Update is called once per frame
        void Update()
        {
            BackgroundMaterial.mainTextureOffset += Vector2.down * (scrollingSpeed * Time.deltaTime);
        }
    }
}