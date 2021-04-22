using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBottle : MonoBehaviour
{
    private SpriteRenderer layerOrder;
    void OnTriggerEnter2D(Collider2D playercollision)
    {
        GameObject.Find("Player 1").GetComponent<PlayerMovement>().damageBoosting = true;
        GameObject.Find("Player 1").GetComponent<PlayerMovement>().DamageMulit = 2;
        GameObject.Find("GameControl").GetComponent<GameControl>().DB = true;
        Destroy(gameObject);
    }

    private void Update()
    {
        layerOrder = GetComponent<SpriteRenderer>();
        layerOrder.sortingOrder = ((int)transform.position.y * -1) - 4;
    }
}
