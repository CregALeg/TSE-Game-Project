using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class starthandler : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
}
