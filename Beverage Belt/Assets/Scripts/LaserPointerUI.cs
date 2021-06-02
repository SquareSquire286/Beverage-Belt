using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserPointerUI : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 start, end;
    public List<GameObject> buttons;
    public Material clearMaterial, highlightMaterial;
    [SerializeField] Text modeText;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        start = transform.position;

        lineRenderer.material.color = Color.grey;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        if (Physics.Raycast(start, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            end = transform.position + transform.forward * hit.distance;

            if (hit.transform.gameObject.layer == 11)
            {
                lineRenderer.material.color = Color.blue;
                modeText.text = hit.transform.gameObject.GetComponent<StartGameMode>().DisplayInfo();
                hit.transform.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = highlightMaterial;

                foreach (GameObject obj in buttons) // prevents multiple buttons from being highlighted at the same time
                {
                    if (obj != hit.transform.gameObject)
                        obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = clearMaterial;
                }

                if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
                    hit.transform.gameObject.GetComponent<StartGameMode>().StartScene();
            }

            else
            {
                end = transform.position + transform.forward * 10f;
                modeText.text = "";

                foreach (GameObject obj in buttons)
                    obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = clearMaterial;
            }
        }
    }
}
