using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPaleteParams : MonoBehaviour
{
    public bool isInstanceReady;
    public bool inEditMode;
    private Material material;


    // Start is called before the first frame update
    private void Start()
    {
        SetMaterialInstance();
    }

    private void OnValidate()
    {
        if(inEditMode)
            SetMaterialInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaterialInstance()
    {
        if (!isInstanceReady)
        {
            isInstanceReady = true;
            var renderer = GetComponent<Renderer>();
            material = Instantiate(renderer.sharedMaterial) as Material;
            Destroy(renderer.material);
            renderer.material = material;
            string direccionShader = material.shader.name;
            renderer.material.name = "Shader " + direccionShader.Split('/')[1] + " (Instanciada)";
        }
        
    }
}
