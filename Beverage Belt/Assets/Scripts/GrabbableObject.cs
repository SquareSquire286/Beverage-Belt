using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private int objectID;
    private bool hasCap;
    private bool capRemoved;

    public void SetObjectID(int id)
    {
        objectID = id;
    }

    public int GetObjectID()
    {
        return objectID;
    }
}
