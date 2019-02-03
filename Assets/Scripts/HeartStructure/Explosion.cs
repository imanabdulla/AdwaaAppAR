using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector3 initialPos, currentPos;
    private Quaternion initialRot, currentRot;
    private Rigidbody rigidbody;
    private bool isExploded;

    public GameObject atmosphere;

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        //disable atmosphere
        atmosphere.SetActive(false);
        //make it kinematic
        rigidbody.isKinematic = true;
        //get initial position& rotation
        initialPos = this.transform.position;
        initialRot = this.transform.rotation;
    }

    public void Explode ()
    {
        //make slow motion
        Time.timeScale = 0.01f;
        //make it dymanic
        rigidbody.isKinematic = false;
        //enable atmosphere
        atmosphere.SetActive(true);
    }

    private void AfterExplosion()
    {
        Time.timeScale = 1f;

        currentPos = this.transform.position;
        currentRot = this.transform.rotation;
        Debug.DrawLine(currentPos, initialPos, Color.red, 20f, false);
        isExploded = true;
    }

    public void Collect()
    {
        //make slow motion
        Time.timeScale = 0.01f;

        //disable atmosphere
        atmosphere.SetActive(false);

        Vector3 dir = initialPos - currentPos;

        //make it dynamic
        rigidbody.constraints = RigidbodyConstraints.None;

        rigidbody.AddForce(dir * 500);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isExploded)
        {
            if (collision.gameObject.layer == 10)
            {
                this.transform.position = initialPos;
                this.transform.rotation = initialRot;
                //make it static
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            Invoke("AfterExplosion", 0.05f);
        }
    }
}
