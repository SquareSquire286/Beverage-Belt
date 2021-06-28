using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClassicGameManager : MonoBehaviour
{
    private float score;
    public ObjectSpawner spawner;
    private int itemsHandled;
    public int livesRemaining;
    public Text items, lives, grade;
    public AudioSource success, error;
    public List<GameObject> beltComponents;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        itemsHandled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lives.text = "Errors Remaining: " + livesRemaining;
        items.text = "Items Handled: " + itemsHandled;

        grade.color = Color.Lerp(Color.red, Color.green, score / 1000f);

        if (itemsHandled > 0)
            grade.text = "$" + (score / 100f).ToString("0.00");

        else grade.text = "$0.00";
    }

    public void Error()
    {
        livesRemaining--;
        spawner.ReduceLives();
        itemsHandled++;
        error.Play();

        foreach (GameObject component in beltComponents)
            component.GetComponent<ConveyorBehavior>().speed = 0f;

        if (livesRemaining > 0)
            Invoke("RestartConveyor", 3);

        else Invoke("ReturnToMenu", 5);
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

    public void Success(int points)
    {
        score += points;
        itemsHandled++;
        success.Play();
    }
}
