using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderModule : MonoBehaviour
{
    [SerializeField] float minX; // The minimum position along the X axis that the slider can occupy
    [SerializeField] float maxX; // The maximum position along the X axis that the slider can occupy
    [SerializeField] Text valueText;
    [SerializeField] Material material;

    public float GetMinX() // simple getter for the minX attribute
    {
        return minX;
    }

    public float GetMaxX() // simple getter for the maxX attribute
    {
        return maxX;
    }

    public float GetRange() // getter for the difference between maxX and minX, allowing the backend to perform percentage calculations
    {
        return (maxX - minX);
    }

    public float GetPercentage()
    {
        return ((transform.position.z - minX) / (maxX - minX));
    }

    void Start()
    {
        gameObject.GetComponent<DefaultMaterial>().SetMaterial(material);
    }

    void Update() // Update is called once per frame.
    {
        if (gameObject.name == "SpawnSlider")
            valueText.text = (float)System.Math.Round(this.GetPercentage() * 5f + 0.5f, 3) + " s";

        else valueText.text = (float)System.Math.Round(this.GetPercentage() * 100, 3) + "%";
    }
}
