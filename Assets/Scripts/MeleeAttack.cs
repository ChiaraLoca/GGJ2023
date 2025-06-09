using UnityEngine;

namespace Level2Player {
    public class MeleeAttack : MonoBehaviour
    {

        [SerializeField] GameObject hitbox;
        

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                hitbox.SetActive(true);
            }
        }
    }
}