using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public Material red;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<DefaultMaterial>().SetMaterial(red);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Press()
    {
        transform.position = new Vector3(transform.position.x, (transform.position.y - 0.035f), transform.position.z);
        gameObject.GetComponent<Renderer>().material = red;
        SceneManager.LoadScene("Menu");
    }
}
