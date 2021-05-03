using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaryMovement : FrankMovement
{
    private int Attacktype;
    private int DamagedMulti;
    protected override void stopChase()
    {
        animator.SetBool("walking", false);
        if (TimeBtwAttack <= 0)
        {
            Attacktype = Random.Range(1, 3);
            Debug.Log(Attacktype);
            if (!isAttacking && !beingDamaged && Attacktype == 1)
            {
                
                isAttacking = true;
                animator.SetBool("lightAttack", true);
                DamagedMulti = damage;
                TimeBtwAttack = startTimeBtwAttack;

            }
            if (!isAttacking && !beingDamaged && Attacktype == 2)
            {
                
                isAttacking = true;
                animator.SetBool("heavyAttack", true);
                DamagedMulti = 2;
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
            sound = GetComponent<AudioSource>();
            sound.volume = 0.1f;
            sound.PlayOneShot(punch);
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsPlayer);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {

                enemiesToDamage[i].GetComponent<PlayerMovement>().TakeDamage(DamagedMulti);
            }
            animator.SetBool("lightAttack", false);
            animator.SetBool("heavyAttack", false);
            isAttacking = false;
        }
    }


}
