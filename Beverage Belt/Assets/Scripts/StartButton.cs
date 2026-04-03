using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public Material green;
    public GameObject spawner, gameManager;
    [SerializeField] string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<DefaultMaterial>().SetMaterial(green);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        transform.position = new Vector3(transform.position.x, (transform.position.y - 0.035f), transform.position.z);
        spawner.GetComponent<ObjectSpawner>().BeginSpawning();
        gameObject.GetComponent<Renderer>().material = green;

        if (sceneName == "100Items")
            gameManager.GetComponent<GameManager>().StartTimer();
    }
}
