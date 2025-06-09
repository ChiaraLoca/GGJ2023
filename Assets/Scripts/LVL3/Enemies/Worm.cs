using UnityEngine;

namespace Level3
{
    [RequireComponent(typeof(Enemy))]
    public class Worm : MonoBehaviour
    {
        private Enemy _enmy;
        private Enemy Enmy => _enmy ??= GetComponent<Enemy>();
    }
}