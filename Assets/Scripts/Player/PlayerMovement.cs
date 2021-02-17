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
        if(Input.GetMouseButton(0))
        {
            isAttacking = true;
            if(vertical != 0 || horizontal != 0)
            {
                vertical = 0;
                horizontal = 0;
                animator.SetFloat("Speed", 0);
            }

            animator.SetTrigger("lightAttack");
        }

        if(vertical != 0 || horizontal != 0 && !isAttacking)
        {
            Vector3 movement = new Vector3(horizontal * Speed, vertical * Speed, 0.0f);
            transform.position = transform.position + movement * Time.deltaTime;
            ChangeDirection(horizontal);
        }

        

    }

    public void AlertObservers(string message)
    {
        if (message == "attackEnded");
        {
            isAttacking = false;
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

}