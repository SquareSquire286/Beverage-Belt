using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BeverageType
{
    Can,
    BeerBottle,
    WaterBottle,
    PopBottle,
    MilkCarton
}

public class Beverage : MonoBehaviour
{
    [SerializeField] BeverageType beverageType;
    [SerializeField] float value;

    private bool hasExtraCondition, extraConditionFulfilled;
    private Renderer renderer;

    private Mesh mesh;
    private Material[] possibleMaterials;

    /*
     * The "constructor" will be called by the new BeverageManufacturer class whenever a new beverage object needs to be spawned in.
     * */
    public void Create(Mesh mesh, Material[] materials, BeverageType beverageType, float value)
    {
        this.mesh = mesh;
        this.possibleMaterials = materials;
        this.value = value;
        this.beverageType = beverageType;
    }

    public BeverageType GetType()
    {
        return beverageType;
    }

    public bool CheckExtraCondition()
    {
        if (hasExtraCondition)
            return extraConditionFulfilled;

        else return hasExtraCondition;
    }
}
