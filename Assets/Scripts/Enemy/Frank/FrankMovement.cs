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

    bool isAttacking;
    float TimeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsPlayer;
    private bool beingDamaged;

    public int damage;

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
        if (TimeBtwAttack <= 0)
        {
            if (!isAttacking && !beingDamaged)
            {
                isAttacking = true;
                animator.SetBool("Attack", true);
                
                TimeBtwAttack = startTimeBtwAttack;

            }
        }
        else
        {
            TimeBtwAttack -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Health <= 0)
        {
            animator.SetBool("isDead", true);
            
        }
        else if(!isAttacking && !beingDamaged)
        {
            targetDistance = Vector2.Distance(transform.position, target.transform.position);
            if (targetDistance < chaseDistance && targetDistance > stopDistance)
            {
                chasePlayer();
                animator.SetBool("walking", true);
            }
            else if (targetDistance > chaseDistance)
            {
                animator.SetBool("walking", false);
            }
            else
            {
                stopChase();


            }
        }


        
            

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }


    public void TakeDamage(int damage)
    {
        if(beingDamaged)
        {

        }
        else
        {
            Health -= damage;
            beingDamaged = true;
            isAttacking = false;
            animator.SetBool("isDamaged?", true);
            Debug.Log("Enemy damaged");
        }
       
    }

    public void AlertObservers(string message)
    {
        if (message == "attackEnded")
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsPlayer);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<PlayerMovement>().TakeDamage(damage);
            }
            animator.SetBool("Attack", false);
            isAttacking = false; 
        }
    }

    public void DamagedObservers(string message)
    {
        if (message == "damageEnded")
        {
            animator.SetBool("isDamaged?", false);
            beingDamaged = false;

        }
    }

    public void DeadObservers(string message)
    {
        if (message == "Dead")
        {
            beingDamaged = false;
            isAttacking = false;
            Destroy(gameObject);
        }
    }
}
