using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FrenzyGameManager : MonoBehaviour
{
    public GameObject spawner;
    private float score, startTime;
    private int sortingErrors, handlingErrors, itemsLost, itemsRemaining;
    public Text grade, time;
    public AudioSource success, countdown;
    private bool timerStarted, countdownStarted;

    // Start is called before the first frame update
    void Start()
    {
        startTime = 999999;
        countdownStarted = false;
        score = 0;
        sortingErrors = 0;
        handlingErrors = 0;
        itemsLost = 0;
        itemsRemaining = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            if (Time.time - startTime < 120f)
                time.text = "Time: " + string.Format("{0:0}", Mathf.FloorToInt((Time.time - startTime) / 3600)) + ":" + string.Format("{0:00}", Mathf.FloorToInt((2 - (Time.time - startTime) / 60))) + ":" + string.Format("{0:00.000}", (float)System.Math.Round((60 - ((Time.time - startTime) % 60)), 3));

            else
            {
                time.text = "Time: 0:00:00.000";
                Invoke("ReturnToMenu", 5);
            }
        }

        else time.text = "Time: 0:02:00.000";

        if (!countdownStarted)
        {
            if (Time.time - startTime >= 110f)
            {
                time.color = Color.red;
                countdown.Play();
                countdownStarted = true;
            }
        }

        //timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (itemsRemaining < 100) // prevents divide by zero error
        {
            Color colour = Color.Lerp(Color.red, Color.green, score / (100 - itemsRemaining));
            grade.color = colour;
            grade.text = "" + score;
        }

        else
        {
            grade.color = Color.red;
            grade.text = "0";
        }
    }

    public void StartTimer()
    {
        timerStarted = true;
        startTime = Time.time;
        spawner.GetComponent<ObjectSpawner>().SetAwakeTime();
    }

    public void Error()
    {
        itemsRemaining--;
        if (itemsRemaining == 99)
            this.StartTimer();
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

        if (itemsRemaining == 99)
            this.StartTimer();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
