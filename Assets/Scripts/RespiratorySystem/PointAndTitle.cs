using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndTitle : MonoBehaviour
{
    private TextMesh textMesh;
    [SerializeField]
    private string objectTitle;

    private void Start ()
    {
        textMesh = this.transform.root.GetComponentInChildren<TextMesh>();
	}
	
    private void OnMouseDown()
    {
        textMesh.text = objectTitle;
        textMesh.transform.position = this.gameObject.name == "Lungs"? this.transform.parent.position : this.transform.position;
    }
}
