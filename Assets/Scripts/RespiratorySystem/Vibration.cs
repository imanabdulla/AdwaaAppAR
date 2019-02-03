using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration : MonoBehaviour {

    private Animator animator;

	void Start ()
    {
        animator = this.gameObject.name == "Lungs" ? this.transform.parent.GetComponent<Animator>(): this.GetComponent<Animator>();
        animator.enabled = false;
    }
    private void OnMouseDown()
    {
        if (!animator.enabled)
        {
            animator.enabled = true;
        }
        animator.SetTrigger("Vibrate");
    }
}
