using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D playercollision)
    {
        GameControl.health += 1;
    }
}
