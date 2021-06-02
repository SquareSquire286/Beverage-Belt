using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerBehaviour : MonoBehaviour
{
    void OnCollisionEnter(Collision col) // Called whenever an object is placed into the drawer
    { // prevent the drawer from being displaced by the object with which it collides
        if (col.gameObject.tag == "GrabPoint")
        {
            col.gameObject.transform.parent = gameObject.transform; // any object colliding with the drawer that is not the player's hands or another drawer will become the drawer's child (and thus move along with the drawer)
        }
    }

    void OnCollisionExit(Collision col) // Called whenever an object leaves the drawer's surface (i.e. when the user picks the object up)
    {
        col.gameObject.transform.parent = null; // the object (which is now in the player's hands) no longer has the drawer has its parent, and can thus be independently manipulated
    }
}
