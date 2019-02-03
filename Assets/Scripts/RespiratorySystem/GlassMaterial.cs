using UnityEngine;

public class GlassMaterial : MonoBehaviour
{
    private MeshRenderer meshRend;

	private void Start ()
    {
        meshRend = this.GetComponent<MeshRenderer>();
	}

    private void OnMouseDown()
    {
        meshRend.material.color = new Color(meshRend.material.color.r, meshRend.material.color.g, meshRend.material.color.b, 0.0f);
    }
}
