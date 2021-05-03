using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOControl : MonoBehaviour
{
    public GameObject GO;
    // Start is called before the first frame update
    void Start()
    {
        GO.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void ShowGO()
    {
        
        if (GameObject.Find("SceneTransition").GetComponent<ScreenSwitch>().isActiveAndEnabled)

            GO.gameObject.SetActive(true);

    }
}
