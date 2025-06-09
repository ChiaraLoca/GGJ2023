using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Level1
{
    public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
}
