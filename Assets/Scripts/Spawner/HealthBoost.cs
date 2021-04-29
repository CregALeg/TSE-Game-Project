using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    private SpriteRenderer layerOrder;
    private AudioSource sound;
    public AudioClip powerup;
    void OnTriggerEnter2D(Collider2D playercollision)
    {

        GameObject.Find("Player 1").GetComponent<PlayerMovement>().health += 5;
        GameObject.Find("Player 1").GetComponent<PlayerMovement>().powerPick();
        Destroy(gameObject);
    }

    private void Update()
    {
        layerOrder = GetComponent<SpriteRenderer>();
        layerOrder.sortingOrder = ((int)transform.position.y * -1) - 4;
    }
}
