using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionModule : MonoBehaviour
{
    private bool grabbed;
    public string hand;
    public GameObject grabbedObject, otherHand;
    private GameObject rightHandModel, leftHandModel;
    public Material grabHighlight, subgrabHighlight, handRender;
    private Vector3 previousPos;
    private Quaternion previousRot;
    public List<GameObject> currentCollisions;

    // Start is called before the first frame update
    void Start()
    {
        currentCollisions = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftHandModel == null)
        {
            leftHandModel = GameObject.Find("hand_left");
            leftHandModel.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = handRender;
        }

        if (rightHandModel == null)
        {
            rightHandModel = GameObject.Find("hand_right");
            rightHandModel.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = handRender;
        }

        if ((!OVRInput.Get(OVRInput.RawButton.LHandTrigger) && hand == "Left") || (!OVRInput.Get(OVRInput.RawButton.RHandTrigger) && hand == "Right"))
        {
            grabbed = false;

            if (grabbedObject != null && grabbedObject.tag != "Handle" && grabbedObject.tag != "Slider")
            {
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                Vector3 velocity = (transform.position - previousPos) / Time.deltaTime;
                Vector3 angularVelocity = (transform.rotation.eulerAngles - previousRot.eulerAngles) / Time.deltaTime;
                rb.drag = 0;
                rb.velocity = velocity;
                rb.angularVelocity = angularVelocity;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                previousPos = transform.position;
                previousRot = transform.rotation;
            }

            if (hand == "Left")
                leftHandModel.SetActive(true);

            else rightHandModel.SetActive(true);

            grabbedObject = null;
        }

        if (grabbed && grabbedObject != null)
        {
            if (grabbedObject.tag == "Handle")
            {
                grabbedObject.transform.position = new Vector3(Mathf.Clamp(transform.position.x, grabbedObject.GetComponent<DrawerHandle>().GetMin(), grabbedObject.GetComponent<DrawerHandle>().GetMax()), grabbedObject.transform.position.y, grabbedObject.transform.position.z);
            }

            else if (grabbedObject.tag == "Slider")
                grabbedObject.transform.position = new Vector3(grabbedObject.transform.position.x, grabbedObject.transform.position.y, Mathf.Clamp(transform.position.z, grabbedObject.GetComponent<SliderModule>().GetMinX(), grabbedObject.GetComponent<SliderModule>().GetMaxX()));

            else
            {
                grabbedObject.transform.position = transform.position;
                grabbedObject.transform.rotation = transform.rotation;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                previousPos = transform.position;
                previousRot = transform.rotation;

                if (hand == "Left")
                    leftHandModel.SetActive(false);

                else rightHandModel.SetActive(false);
            }      
        }

        if ((OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger) && hand == "Left") || (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && hand == "Right"))
        {
            foreach (GameObject obj in currentCollisions)
            {
                if (obj.layer == 10)
                {
                    currentCollisions.Remove(obj);
                    obj.transform.parent.gameObject.GetComponent<RemovableObjectHolder>().ObjectRemoved();
                    Destroy(obj);
                    break;
                }

                else if (obj.layer == 11)
                {
                    if (obj.GetComponent<StartButton>() != null)
                    {
                        obj.GetComponent<StartButton>().Press();
                        obj.layer = 0;
                        currentCollisions.Remove(obj);
                        break;
                    }

                    else if (obj.GetComponent<MenuButton>() != null) // this should resolve a bug wherein the menu button would be inadvertently pressed when attempting to remove a cap or pop tab
                    {
                        if (grabbedObject == null && otherHand.GetComponent<InteractionModule>().grabbedObject == null)
                        {
                            obj.GetComponent<MenuButton>().Press();
                            obj.layer = 0;
                            currentCollisions.Remove(obj);
                            break;
                        }
                    }
                }
            }
        }

        if (grabbedObject == null)
        {
            if (!grabbed && ((OVRInput.Get(OVRInput.RawButton.LHandTrigger) && hand == "Left") || (OVRInput.Get(OVRInput.RawButton.RHandTrigger) && hand == "Right")))
            {
                foreach (GameObject obj in currentCollisions)
                {
                    if (obj != null && obj.layer == 9)
                    {
                        if (obj.tag == "GrabPoint")
                            obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = obj.GetComponent<DefaultMaterial>().GetMaterial();

                        else obj.GetComponent<Renderer>().material = obj.GetComponent<DefaultMaterial>().GetMaterial();

                        grabbed = true;
                        grabbedObject = obj;
                        break;
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 9 && grabbedObject == null && col.gameObject != otherHand.GetComponent<InteractionModule>().grabbedObject)
        {
            if (col.gameObject.tag == "GrabPoint") // we only want to highlight grabbable objects if the user is not currently holding an object
                col.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = grabHighlight;

            else col.gameObject.GetComponent<Renderer>().material = grabHighlight;

            currentCollisions.Add(col.gameObject);
        }

        else if (col.gameObject.layer == 10)
        {
            if (col.gameObject.transform.parent.gameObject == otherHand.GetComponent<InteractionModule>().grabbedObject)
            {
                col.gameObject.GetComponent<Renderer>().material = subgrabHighlight;
                currentCollisions.Add(col.gameObject);
            }     
        }

        else if (col.gameObject.layer == 11)
        {
            col.gameObject.GetComponent<Renderer>().material = subgrabHighlight;
            currentCollisions.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == 9)
        {
            if (col.gameObject.tag == "GrabPoint")
                col.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = col.gameObject.GetComponent<DefaultMaterial>().GetMaterial();

            else col.gameObject.GetComponent<Renderer>().material = col.gameObject.GetComponent<DefaultMaterial>().GetMaterial();

            currentCollisions.Remove(col.gameObject);
        }

        else if (col.gameObject.layer == 10)
        {
            col.gameObject.GetComponent<Renderer>().material = col.gameObject.GetComponent<DefaultMaterial>().GetMaterial();
            currentCollisions.Remove(col.gameObject);
        }

        else if (col.gameObject.layer == 11)
        {
            col.gameObject.GetComponent<Renderer>().material = col.gameObject.GetComponent<DefaultMaterial>().GetMaterial();
            currentCollisions.Add(col.gameObject);
        }
    }
}