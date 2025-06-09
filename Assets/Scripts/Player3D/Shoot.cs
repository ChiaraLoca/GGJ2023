using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Level1
{
    public class Shoot : MonoBehaviour
{

    [SerializeField] private bool _canShoot =true;
    [SerializeField] private float _reloadTime;
    [SerializeField]private  GameObject _projectilePrefab;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && _canShoot)
        {
            shoot();
            StartCoroutine(WaitAndReload());
        }
    }

    public void shoot()
    {
        GameObject tmp = Instantiate(_projectilePrefab, transform);
        tmp.transform.localPosition += new Vector3(0f, 1f, 0);
        tmp.GetComponent<Rigidbody>().AddForce(transform.forward*10, ForceMode.Impulse);
        _canShoot = false;


    }
    private IEnumerator WaitAndReload()
    {
        yield return new WaitForSeconds(_reloadTime);
        _canShoot = true;
    }
}}
