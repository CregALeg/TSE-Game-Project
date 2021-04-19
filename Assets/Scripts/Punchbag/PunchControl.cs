using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchControl : FrankMovement
{


    protected override void movementManager()
    {
        layerOrder = GetComponent<SpriteRenderer>();
        layerOrder.sortingOrder = (int)transform.position.y * -1;

        flipDistance = transform.position.x - target.position.x;


        if (!beingDamaged)
        {
            targetDistance = Vector2.Distance(transform.position, target.position);
            chasePlayer();

        }
    }

    protected override void chasePlayer()
    {
        if (flipDistance > 0 && direction || flipDistance < 0 && !direction)
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

    }

    public override void TakeDamage(int damage)
    {
        Debug.Log("Being damaged1 ");
        if (beingDamaged)
        {
            Debug.Log("Being damaged2 ");
        }
        else
        {
            beingDamaged = true;

            animator.SetBool("isDamaged?", true);
            Debug.Log("Enemy damaged");


        }

    }
}
