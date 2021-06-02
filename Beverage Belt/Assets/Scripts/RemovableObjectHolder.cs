using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovableObjectHolder : MonoBehaviour
{
    private bool objectExists;
    // Start is called before the first frame update
    void Start()
    {
        objectExists = true;
    }

    public bool GetObjectStatus()
    {
        return objectExists;
    }

    public void ObjectRemoved()
    {
        objectExists = false;
    }
}
