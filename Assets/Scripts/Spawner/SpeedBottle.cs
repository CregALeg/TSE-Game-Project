using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBottle : MonoBehaviour
{
    private SpriteRenderer layerOrder;

    void OnTriggerEnter2D(Collider2D playercollision)
    {

       GameObject.Find("Player 1").GetComponent<PlayerMovement>().speedBoosting = true;
       GameObject.Find("Player 1").GetComponent<PlayerMovement>().Speed = 20;
       GameObject.Find("Player 1").GetComponent<PlayerMovement>().powerPick();
       GameObject.Find("GameControl").GetComponent<GameControl>().SB = true;
       Destroy(gameObject);
    }

    private void Update()
    {
        layerOrder = GetComponent<SpriteRenderer>();
        layerOrder.sortingOrder = ((int)transform.position.y * -1) - 4;
    }
}

