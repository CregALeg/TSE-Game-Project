using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightAttack : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) )
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("lightAttack");
    }
}
