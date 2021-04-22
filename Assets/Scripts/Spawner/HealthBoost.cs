using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    private SpriteRenderer layerOrder;
    void OnTriggerEnter2D(Collider2D playercollision)
    {
        //GameControl.health += 1;
        GameObject.Find("Player 1").GetComponent<PlayerMovement>().health += 1;
        Destroy(gameObject);
    }

    private void Update()
    {
        layerOrder = GetComponent<SpriteRenderer>();
        layerOrder.sortingOrder = (int)transform.position.y * -1;
    }
}
