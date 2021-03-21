using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaryMovement : FrankMovement
{
    private int Attacktype;
    protected override void stopChase()
    {
        animator.SetBool("walking", false);
        if (TimeBtwAttack <= 0)
        {
            Attacktype = Random.Range(1, 2);
            if (!isAttacking && !beingDamaged && Attacktype == 1)
            {
                
                isAttacking = true;
                animator.SetBool("lightAttack", true);

                TimeBtwAttack = startTimeBtwAttack;

            }
            if (!isAttacking && !beingDamaged && Attacktype == 2)
            {
                
                isAttacking = true;
                animator.SetBool("heavyAttack", true);

                TimeBtwAttack = startTimeBtwAttack;

            }
        }
        else
        {
            TimeBtwAttack -= Time.deltaTime;
        }
    }

    public override void AlertObservers(string message)
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
            animator.SetBool("lightAttack", false);
            animator.SetBool("heavyAttack", false);
            isAttacking = false;
        }
    }


}
