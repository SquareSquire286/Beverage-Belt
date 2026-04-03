using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<GrabbableObject>() != null)
        {

        }
    }

    public void EvaluateCollision()
    {

    }
}
