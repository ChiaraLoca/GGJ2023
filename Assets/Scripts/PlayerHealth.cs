using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int _playerHealth;
    [SerializeField] Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = 1;
    }

    public void Damage()
    {
        if (--_playerHealth < 1)
            Death();

    }

    private void Death()
    {
        _playerHealth = 1;
        transform.position = respawnPoint.transform.position;
    }
}
