using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Level1
{
    public class MoveCamera3D : MonoBehaviour
{


    public float speed = 3.5f;
    private float X;
    private float Y;

    void Update()
    {
        /*if (Input.GetMouseButton(0))
        {*/
            transform.Rotate(new Vector3(/*Input.GetAxis("Mouse Y") * speed*/0, Input.GetAxis("Mouse X") * speed, 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(X, Y, 0);
        //}
    }
}}
