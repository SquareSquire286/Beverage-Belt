using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float score, startTime;
    private int sortingErrors, handlingErrors, itemsLost, itemsRemaining;
    public Text items, sort, handle, grade, time;
    public AudioSource success, failure;
    private bool timerStarted;
    public List<GameObject> beltComponents;
    [SerializeField] string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        sortingErrors = 0;
        handlingErrors = 0;
        itemsLost = 0;
        itemsRemaining = 100;
    }

    // Update is called once per frame
    void Update()
    {
        items.text = "Items Lost: " + itemsLost;
        sort.text = "Sorting Errors: " + sortingErrors;
        handle.text = "Handling Errors: " + handlingErrors;

        if (timerStarted)
        {
            if (itemsRemaining > 0 || sceneName == "FreePlay")
                time.text = "Time: " + string.Format("{0:0}", Mathf.FloorToInt((Time.time - startTime) / 3600)) + ":" + string.Format("{0:00}", Mathf.FloorToInt((Time.time - startTime) / 60)) + ":" + string.Format("{0:00.000}", (float)System.Math.Round((Time.time - startTime) % 60, 3));
        }

        else time.text = "Time: 0:00:00.000";

        //timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (itemsRemaining < 100) // prevents divide by zero error
        {
            Color colour = Color.Lerp(Color.red, Color.green, score / (100 - itemsRemaining));
            grade.color = colour;
            float percentage = (score / (100 - itemsRemaining)) * 100;
            percentage = (float)System.Math.Round(percentage, 0);
            grade.text = score + "/" + (100 - itemsRemaining) + " = " + percentage + "%";
        }

        else
        {
            grade.color = Color.red;
            grade.text = "0/0 = 0%";
        }

        if (sceneName == "FreePlay")
        {
            foreach (GameObject component in beltComponents)
            {
                component.GetComponent<ConveyorBehavior>().speed = GameObject.Find("BeltSlider").GetComponent<SliderModule>().GetPercentage();
            }
        }
    }

    public void StartTimer()
    {
        timerStarted = true;
        startTime = Time.time;
    }

    public void Error()
    {
        itemsRemaining--;
        failure.Play();

        if (sceneName != "FreePlay")
        {
            foreach (GameObject component in beltComponents)
                component.GetComponent<ConveyorBehavior>().speed = 0f;

            Invoke("RestartConveyor", 3);

            if (itemsRemaining == 0)
                Invoke("ReturnToMenu", 5);
        }
    }

    public void RestartConveyor()
    {
        foreach (GameObject component in beltComponents)
        {
            if (component.GetComponent<BeltBehavior>() != null)
                component.GetComponent<BeltBehavior>().speed = 0.4f;

            else component.GetComponent<RollerBehavior>().speed = 0.64f;
        }
    }

    public void ItemLost()
    {
        itemsLost++;
        Error();
    }

    public void SortingError()
    {
        sortingErrors++;
        Error();
    }

    public void HandlingError()
    {
        handlingErrors++;
        Error();
    }

    public void Success()
    {
        score++;
        itemsRemaining--;
        success.Play();

        if (itemsRemaining == 0 && sceneName != "FreePlay")
            Invoke("ReturnToMenu", 5);
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
