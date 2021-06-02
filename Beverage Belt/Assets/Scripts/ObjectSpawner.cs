using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Material[] canMaterials, bottleMaterials, capMaterials;
    public GameObject abstractCan, abstractTablessCan, abstractBottle, abstractCaplessBottle, abstractCarton, abstractBeerBottle;
    private List<GameObject> beverageList;
    public int itemCount, classicLivesLeft, survivalLivesLeft;
    private bool spawn;
    private bool gameCondition;
    private float startTime, awakeTime;
    public float spawnDelayTime;
    [SerializeField] string sceneName;

    void Start()
    {
        beverageList = new List<GameObject>();
        awakeTime = 9999999; // sentinel value to prevent accidental termination of the Frenzy mode scene
        startTime = 0;
        classicLivesLeft = 3;
        survivalLivesLeft = 1;
        spawn = false;
    }
    public void BeginSpawning()
    {
        spawn = true;
    }

    public void SetAwakeTime()
    {
        awakeTime = Time.time;
    }

    public void ReduceLives()
    {
        classicLivesLeft--;
        survivalLivesLeft--;
    }

    void Update()
    {
        if (sceneName == "100Items")
        {
            gameCondition = (itemCount < 100);
            spawnDelayTime = 2.5f - (3 * itemCount / 200.0f);
        }

        else if (sceneName == "ClassicMode" || sceneName == "SurvivalMode")
        {
            if (itemCount < 100)
                spawnDelayTime = 2.5f - (3 * itemCount / 200.0f);

            else if (itemCount >= 100 && itemCount < 150)
                spawnDelayTime = 1.0f;

            else spawnDelayTime = 0.5f;

            if (sceneName == "ClassicMode")
                gameCondition = (classicLivesLeft > 0);

            else gameCondition = (survivalLivesLeft > 0);
        }

        else if (sceneName == "FranticMode")
        {
            spawnDelayTime = 0.9f;
            gameCondition = ((Time.time - awakeTime) < 120f);
        }

        else if (sceneName == "FreePlay")
        {
            gameCondition = true;
            spawnDelayTime = (GameObject.Find("SpawnSlider").GetComponent<SliderModule>().GetPercentage() * 5f) + 0.5f; // the spawn delay time is limited between 0.5 and 5.5 seconds.
        }

        if (spawn && gameCondition)
        {
            Invoke("CreateObject", spawnDelayTime);
            spawn = false;
        }

        if (!gameCondition)
        {
            if (sceneName != "100Items")
                this.DestroyObjects();
        }
    }

    void DestroyObjects()
    {
        foreach (GameObject obj in beverageList)
        {
            if (obj != null)
            {
                obj.layer = 0;
                beverageList.Remove(obj);    
                Destroy(obj);
            }
        }
    }

    public void DestroyGrabbedObject(GameObject obj) // called by ObjectDestroyer
    {
        beverageList.Remove(obj);
    }

    void CreateObject()
    {
        if (gameCondition)
        {
            int index = Random.Range(0, 4);

            switch (index)
            {
                case 0: CreateCan(Random.Range(0, 2), Random.Range(0, canMaterials.Length)); break;
                case 1: CreateMilkCarton(); break;
                case 2: CreateBeerBottle(); break;
                default: CreateBottle(Random.Range(0, 2), Random.Range(0, bottleMaterials.Length), Random.Range(0, capMaterials.Length)); break;
            }
        }

        itemCount++;
        startTime = Time.time;
        spawn = true;
    }

    void CreateCan(int hasPopTab, int index)
    {
        if (hasPopTab == 1)
        {
            GameObject thisCan = Instantiate(abstractCan, transform.position, transform.rotation);
            thisCan.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = canMaterials[index];
            thisCan.GetComponent<DefaultMaterial>().SetMaterial(canMaterials[index]);
            thisCan.transform.GetChild(1).gameObject.GetComponent<DefaultMaterial>().SetMaterial(thisCan.transform.GetChild(1).gameObject.GetComponent<Renderer>().material);
            thisCan.GetComponent<GrabbableObject>().SetObjectID(2);

            beverageList.Add(thisCan);
        }

        else
        {
            GameObject thisCan = Instantiate(abstractTablessCan, transform.position, transform.rotation);
            thisCan.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = canMaterials[index];
            thisCan.GetComponent<DefaultMaterial>().SetMaterial(canMaterials[index]);
            thisCan.GetComponent<GrabbableObject>().SetObjectID(2);

            beverageList.Add(thisCan);
        }
    }

    void CreateMilkCarton()
    {
        GameObject thisCarton = Instantiate(abstractCarton, transform.position, transform.rotation);
        thisCarton.GetComponent<DefaultMaterial>().SetMaterial(thisCarton.transform.GetChild(0).gameObject.GetComponent<Renderer>().material);
        thisCarton.GetComponent<GrabbableObject>().SetObjectID(3);

        beverageList.Add(thisCarton);
    }

    void CreateBeerBottle()
    {
        GameObject thisBooze = Instantiate(abstractBeerBottle, transform.position, transform.rotation);
        thisBooze.GetComponent<DefaultMaterial>().SetMaterial(thisBooze.transform.GetChild(0).gameObject.GetComponent<Renderer>().material);
        thisBooze.GetComponent<GrabbableObject>().SetObjectID(4);

        beverageList.Add(thisBooze);
    }

    void CreateBottle(int hasCap, int index1, int index2)
    {
        if (hasCap == 0)
        {
            GameObject thisBottle = Instantiate(abstractCaplessBottle, transform.position, transform.rotation);
            thisBottle.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = bottleMaterials[index1];
            thisBottle.GetComponent<DefaultMaterial>().SetMaterial(bottleMaterials[index1]);

            if (index1 == 0)
                thisBottle.GetComponent<GrabbableObject>().SetObjectID(index1);

            else thisBottle.GetComponent<GrabbableObject>().SetObjectID(1);

            beverageList.Add(thisBottle);
        }

        else
        {
            GameObject thisBottle = Instantiate(abstractBottle, transform.position, transform.rotation);
            thisBottle.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = bottleMaterials[index1];
            thisBottle.GetComponent<DefaultMaterial>().SetMaterial(bottleMaterials[index1]);
            thisBottle.GetComponent<GrabbableObject>().SetObjectID(index1);

            if (index1 == 3) // indicating a red bottle, which MUST be matched with a red lid
            {
                thisBottle.transform.GetChild(2).gameObject.GetComponent<Renderer>().material = capMaterials[3];
                thisBottle.transform.GetChild(3).gameObject.GetComponent<Renderer>().material = capMaterials[3];
                thisBottle.transform.GetChild(2).gameObject.GetComponent<DefaultMaterial>().SetMaterial(capMaterials[3]);
                thisBottle.GetComponent<GrabbableObject>().SetObjectID(1);

                beverageList.Add(thisBottle);
            }

            else if (index1 == 2) // indicating a green bottle, which MUST be matched with a green lid
            {
                thisBottle.transform.GetChild(2).gameObject.GetComponent<Renderer>().material = capMaterials[2];
                thisBottle.transform.GetChild(3).gameObject.GetComponent<Renderer>().material = capMaterials[2];
                thisBottle.transform.GetChild(2).gameObject.GetComponent<DefaultMaterial>().SetMaterial(capMaterials[2]);
                thisBottle.GetComponent<GrabbableObject>().SetObjectID(1);

                beverageList.Add(thisBottle);
            }

            else if (index1 == 1) // indicating a blue bottle, which MUST be matched with a blue lid
            {
                thisBottle.transform.GetChild(2).gameObject.GetComponent<Renderer>().material = capMaterials[1];
                thisBottle.transform.GetChild(3).gameObject.GetComponent<Renderer>().material = capMaterials[1];
                thisBottle.transform.GetChild(2).gameObject.GetComponent<DefaultMaterial>().SetMaterial(capMaterials[1]);
                thisBottle.GetComponent<GrabbableObject>().SetObjectID(1);

                beverageList.Add(thisBottle);
            }

            else // indicating a clear bottle, which can be matched with any lid
            {
                thisBottle.transform.GetChild(2).gameObject.GetComponent<Renderer>().material = capMaterials[index2];
                thisBottle.transform.GetChild(3).gameObject.GetComponent<Renderer>().material = capMaterials[index2];
                thisBottle.transform.GetChild(2).gameObject.GetComponent<DefaultMaterial>().SetMaterial(capMaterials[index2]);
                thisBottle.GetComponent<GrabbableObject>().SetObjectID(0);

                beverageList.Add(thisBottle);
            }

            
        }
    }
}
