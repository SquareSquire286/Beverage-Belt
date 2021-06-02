using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMaterial : MonoBehaviour
{
    private Material defaultMaterial;

    public void SetMaterial(Material material)
    {
        defaultMaterial = material;
    }

    public Material GetMaterial()
    {
        return defaultMaterial;
    }
}
