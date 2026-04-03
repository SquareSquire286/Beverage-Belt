using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public GameObject leftHand, rightHand, gameManager, spawner;
    [SerializeField] int destroyID;
    private int thisObjID, thisObjPointTotal;
    private bool lidRemoved;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "GrabPoint" && col.gameObject != leftHand.GetComponent<InteractionModule>().grabbedObject && col.gameObject != rightHand.GetComponent<InteractionModule>().grabbedObject)
        {
            col.gameObject.layer = 0; // prevent the player from grabbing the object after it has been placed in the destroy queue
            col.gameObject.GetComponent<Collider>().enabled = false;
            thisObjID = col.gameObject.GetComponent<GrabbableObject>().GetObjectID();
            thisObjPointTotal = col.gameObject.GetComponent<GrabbableObject>().GetPointValue();

            if (col.gameObject.GetComponent<RemovableObjectHolder>() != null && col.gameObject.GetComponent<RemovableObjectHolder>().GetObjectStatus())
                lidRemoved = false;

            else lidRemoved = true;

            spawner.GetComponent<ObjectSpawner>().DestroyGrabbedObject(col.gameObject);
            Destroy(col.gameObject);

            this.UpdateGameManager(thisObjPointTotal);
        }
    }

    void UpdateGameManager(int pointsToAdd)
    {
        if (gameManager.GetComponent<GameManager>() != null)
        {
            if (destroyID == 6)
                gameManager.GetComponent<GameManager>().ItemLost();

            else if (destroyID != thisObjID)
                gameManager.GetComponent<GameManager>().SortingError();

            else if (destroyID == thisObjID && !lidRemoved)
                gameManager.GetComponent<GameManager>().HandlingError();

            else gameManager.GetComponent<GameManager>().Success();
        }

        else if (gameManager.GetComponent<ClassicGameManager>() != null)
        {
            if (destroyID == 6 || destroyID != thisObjID || (destroyID == thisObjID && !lidRemoved))
                gameManager.GetComponent<ClassicGameManager>().Error();

            else gameManager.GetComponent<ClassicGameManager>().Success(pointsToAdd);
        }

        else if (gameManager.GetComponent<FrenzyGameManager>() != null)
        {
            if (destroyID == 6)
                gameManager.GetComponent<FrenzyGameManager>().ItemLost();

            else if (destroyID != thisObjID)
                gameManager.GetComponent<FrenzyGameManager>().SortingError();

            else if (destroyID == thisObjID && !lidRemoved)
                gameManager.GetComponent<FrenzyGameManager>().HandlingError();

            else gameManager.GetComponent<FrenzyGameManager>().Success();
        }
    }
}
