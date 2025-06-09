using UnityEngine;

namespace Level3
{
    public class ClearEnemy : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}