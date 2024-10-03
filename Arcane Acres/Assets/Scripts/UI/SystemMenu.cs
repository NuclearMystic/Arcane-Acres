using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemMenu : MonoBehaviour
{
    public string nextScene;

    public void StartGame()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void Options()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
