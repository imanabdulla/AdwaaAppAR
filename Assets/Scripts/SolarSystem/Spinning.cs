using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float speed = 50f;
    public Vector3 direction;

    private Transform trans;

    private void Start()
    {
        trans = this.transform;
    }
    void Update ()
    {
        trans.Rotate(direction * speed * Time.deltaTime);
	}
}
