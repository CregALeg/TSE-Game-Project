using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public int Speed = 5;
    public Animator animator;
    bool direction;
    float horizontal;
    float vertical;
    bool isAttacking;
    float TimeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int damage;
    public int health;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(horizontal != 0 ? horizontal : vertical));



        

    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            animator.SetBool("isDead", true);

        }


        if (TimeBtwAttack <= 0)
        {
            if (Input.GetMouseButton(0) && isAttacking == false)
            {
                isAttacking = true;
                if (vertical != 0 || horizontal != 0)
                {
                    vertical = 0;
                    horizontal = 0;
                    animator.SetFloat("Speed", 0);
                    
                }

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<FrankMovement>().TakeDamage(damage);
                }
                animator.SetTrigger("lightAttack");
                TimeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            TimeBtwAttack -= Time.deltaTime;
        }
        

        if(!isAttacking)
        {
            Vector3 movement = new Vector3(horizontal * Speed, vertical * Speed, 0.0f);
            transform.position = transform.position + movement * Time.deltaTime;
            ChangeDirection(horizontal);
        }

        

    }

    

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void ChangeDirection(float horizontal)
    {
        if (horizontal < 0 && !direction || horizontal > 0 && direction)
        {
            direction = !direction;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void TakeDamage(int IncomingDamage)
    {
        health -= IncomingDamage;
        animator.SetBool("isDamaged", true);
        Debug.Log("Player damaged");
    }

    public void AlertObservers(string message)
    {
        if (message == "attackEnded")
        {
            isAttacking = false;
        }
    }


    public void DamagedObservers(string message)
    {
        if (message == "damageEnded")
        {
            animator.SetBool("isDamaged", false);
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