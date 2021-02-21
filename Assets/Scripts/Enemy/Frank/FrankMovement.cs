using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrankMovement : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
    public float stopDistance;
    public GameObject target;
    public float Health;
    private bool Damaged;

    private float targetDistance;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isDamaged?", false);
        animator.SetBool("isDead", false);
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
        

        if (Health <= 0)
        {
            animator.SetBool("isDead", true);
            
        }


        targetDistance = Vector2.Distance(transform.position, target.transform.position);
        if (targetDistance < chaseDistance && targetDistance > stopDistance)
        {
            chasePlayer();
            animator.SetBool("walking", true);
        }
        else
            stopChase();

    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        animator.SetBool("isDamaged?", true);
        
    }

    public void AlertObservers(string message)
    {
        if (message == "damageEnded")
        {
            animator.SetBool("isDamaged?", false);
        }
    }

    public void DeadObservers(string message)
    {
        if (message == "Dead")
        {
            
            Destroy(gameObject);
        }
    }
}
