using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerHandle : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] float min, max;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<DefaultMaterial>().SetMaterial(material);
    }

    public float GetMin()
    {
        return min;
    }

    public float GetMax()
    {
        return max;
    }
}
