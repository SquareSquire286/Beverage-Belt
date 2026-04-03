using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private int objectID;
    [SerializeField] int pointValue;
    private bool hasCap;
    private bool capRemoved;

    public void SetObjectID(int id)
    {
        objectID = id;
    }

    public void SetPointValue(int value)
    {
        pointValue = value;
    }

    public int GetObjectID()
    {
        return objectID;
    }

    public int GetPointValue()
    {
        return pointValue;
    }
}
