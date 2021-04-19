using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSwitch : MonoBehaviour
{
    public int SceneNumber;

    void OnTriggerEnter2D(Collider2D playercollision)
    {
        Debug.Log("Hit");
        SceneManager.LoadScene(SceneNumber);
    }
}
