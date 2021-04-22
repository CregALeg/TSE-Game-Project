using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class starthandler : MonoBehaviour
{
    public int sceneNum;
    public void startGame()
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void close()
    {
        Application.Quit();
    }
}
