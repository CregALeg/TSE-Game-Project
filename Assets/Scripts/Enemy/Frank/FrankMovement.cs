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
        animator.SetBool("isDamaged?", false);

        if (Health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Dead");
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
        Debug.Log("Damage Taken");
    }

}
