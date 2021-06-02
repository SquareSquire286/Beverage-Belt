using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameMode : MonoBehaviour
{
    [SerializeField] string sceneName, modeDetails;

    public void StartScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public string DisplayInfo()
    {
        return modeDetails;
    }
}
