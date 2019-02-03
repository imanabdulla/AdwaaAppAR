using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public float speed;
    private bool isMoved;
    private float r, theta;
    private Vector3 origin;

    private void Update ()
    {
        if (isMoved)
        {
            theta += speed * Time.deltaTime;
            float x = r * (Mathf.Cos(theta)) + origin.x;
            float z = r * (Mathf.Sin(theta)) + origin.z;
            transform.position = new Vector3(x, this.transform.position.y, z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Orbit" + this.gameObject.name)
        {
            r = other.bounds.size.x/2;
            origin = other.gameObject.transform.position;
            isMoved = true;

        }
    }
}
