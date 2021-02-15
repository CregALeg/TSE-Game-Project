using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrankMovement : MonoBehaviour
{
    public float speed;
    public float chase;
    public float stop;
    public GameObject target;

    private float targetDistance;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void chasePlayer()
    {
        if (transform.position.x < target.transform.position.x)
            GetComponent<SpriteRenderer>().flipX = false;
        else
            GetComponent<SpriteRenderer>().flipX = true;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void stopChase()
    {
        animator.SetBool("walking", false);
    }

    // Update is called once per frame
    void Update()
    {
        targetDistance = Vector2.Distance(transform.position, target.transform.position);
        if (targetDistance < chase && targetDistance > stop)
            chasePlayer();
        else
            stopChase();
        animator.SetBool("walking", true);
    }
}
