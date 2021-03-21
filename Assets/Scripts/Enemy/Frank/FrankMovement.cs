using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrankMovement : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
    public float stopDistance;
    protected Transform target;
    public float Health;
    protected SpriteRenderer layerOrder;
    protected bool isAttacking;
    protected float TimeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask whatIsPlayer;
    protected bool beingDamaged;
    protected bool direction;
    public int damage;
    protected float targetDistance;
    public Animator animator;
    public int tokenValue;
    public bool facingRight;
    public float flipDistance;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        target = GameObject.Find("Player 1").GetComponent<Transform>();
        facingRight = true;

    }

    protected virtual void chasePlayer()
    {
        if (flipDistance > 0 && !direction || flipDistance < 0 && direction)
        {

            direction = !direction;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            if (facingRight == true)
            {
                Debug.Log("Facing Left");
                facingRight = false;
            }
            else
            {
                Debug.Log("Facing Right");
                facingRight = true;
            }
        }



        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    protected virtual void stopChase()
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

        movementManager();

    }

    protected virtual void movementManager()
    {
        layerOrder = GetComponent<SpriteRenderer>();
        layerOrder.sortingOrder = (int)transform.position.y * -1;

        flipDistance = transform.position.x - target.position.x;

        if (Health <= 0)
        {

            animator.SetBool("isDead", true);

        }
        else if (!isAttacking && !beingDamaged)
        {

            targetDistance = Vector2.Distance(transform.position, target.position);
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
        Gizmos.DrawWireCube(attackPos.position, new Vector2(attackRangeX, attackRangeY));
    }


    public void TakeDamage(int damage)
    {
        Debug.Log("Being damaged");
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

            if (facingRight == true)
            {
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 15);
            }
            else
            {
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 15);
            }
        }
       
    }

    public virtual void AlertObservers(string message)
    {
        if (message == "attackEnded")
        {
            Debug.Log("Enemy attack");
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsPlayer);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                Debug.Log("Player found");
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
