using UnityEngine;
using System.Collections;

public class Outliner : MonoBehaviour
{

    public Color meshColor = new Color(1f, 1f, 1f, 0.5f);
    public Color outlineColor = new Color(1f, 1f, 0f, 1f);

    // Use this for initialization
    public void Start()
    {
       // Set the transparent material to this object
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;
        int materialsNum = materials.Length;
        for (int i = 0; i < materialsNum; i++)
        {
            materials[i].shader = Shader.Find("Outline/Transparent");
            materials[i].SetColor("_color", meshColor);
        }

        // Create copy of this object, this will have the shader that makes the real outline
        GameObject outlineObj = new GameObject();
        outlineObj.transform.position = transform.position;
        outlineObj.transform.rotation = transform.rotation;
        outlineObj.AddComponent<MeshFilter>();
        outlineObj.AddComponent<MeshRenderer>();
        Mesh mesh;
        mesh = (Mesh)Instantiate(GetComponent<MeshFilter>().mesh);
        outlineObj.GetComponent<MeshFilter>().mesh = mesh;

        outlineObj.transform.parent = this.transform;
        materials = new Material[materialsNum];
        for (int i = 0; i < materialsNum; i++)
        {
            materials[i] = new Material(Shader.Find("Outline/Outline"));
            materials[i].SetColor("_OutlineColor", outlineColor);
        }
        outlineObj.GetComponent<MeshRenderer>().materials = materials;

    }

}