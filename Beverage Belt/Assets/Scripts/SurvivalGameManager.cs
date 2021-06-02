using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SurvivalGameManager : MonoBehaviour
{
    private float score;
    public GameObject spawner;
    private int itemsHandled, livesRemaining;
    public Text items, lives, grade;
    public AudioSource success, error;
    public List<GameObject> beltComponents;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        livesRemaining = 1;
        itemsHandled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lives.text = "Errors Remaining: " + livesRemaining;
        items.text = "Items Handled: " + itemsHandled;

        if (itemsHandled > 0) // prevents divide by zero error
        {
            Color colour = Color.Lerp(Color.red, Color.green, score / itemsHandled);
            grade.color = colour;
            float percentage = (score / itemsHandled) * 100;
            percentage = (float)System.Math.Round(percentage, 0);
            grade.text = score + "/" + itemsHandled + " = " + percentage + "%";
        }

        else
        {
            grade.color = Color.red;
            grade.text = "0/0 = 0%";
        }
    }

    public void Error()
    {
        livesRemaining--;
        spawner.GetComponent<ObjectSpawner>().ReduceLives();
        itemsHandled++;
        error.Play();

        foreach (GameObject component in beltComponents)
            component.GetComponent<ConveyorBehavior>().speed = 0f;

        Invoke("ReturnToMenu", 5);
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

    void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Success()
    {
        score++;
        itemsHandled++;
        success.Play();
    }
}
