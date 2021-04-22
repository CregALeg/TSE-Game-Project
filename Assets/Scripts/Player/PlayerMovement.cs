using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask whatIsEnemy;
    public int damage;
    public int health;
    private bool isDamage;
    private bool dead;
    private int comboCount;
    private SpriteRenderer layerOrder;
    public AudioSource death;
    public AudioClip sound;
    private bool kick;


    // Start is called before the first frame update
    void Start()
    {
        isDamage = false;
        dead = false;
        comboCount = 0;
        death = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        layerOrder = GetComponent<SpriteRenderer>();
        layerOrder.sortingOrder = (int)transform.position.y * -1;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(horizontal != 0 ? horizontal : vertical));



        

    }

    void FixedUpdate()
    {
        if (health < 1)
        {
            animator.SetBool("isDead", true);
            death.PlayOneShot(sound);
            dead = true;

        }


        if (TimeBtwAttack <= 0)
        {
            if (Input.GetMouseButton(0) && !isAttacking && !isDamage && !dead)
            {
                isAttacking = true;
                kick = false;
                if (vertical != 0 || horizontal != 0)
                {
                    vertical = 0;
                    horizontal = 0;
                    animator.SetFloat("Speed", 0);
                    
                }

                comboCount++;
                animator.SetInteger("comboCount", comboCount);
                animator.SetTrigger("lightAttack");
                TimeBtwAttack = startTimeBtwAttack;
            }

            else if (Input.GetMouseButton(1) && !isAttacking && !isDamage && !dead)
            {
                isAttacking = true;
                kick = true;
                if (vertical != 0 || horizontal != 0)
                {
                    vertical = 0;
                    horizontal = 0;
                    animator.SetFloat("Speed", 0);

                }

                comboCount++;
                animator.SetInteger("comboCount", comboCount);
                animator.SetTrigger("heavyAttack");
                TimeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            TimeBtwAttack -= Time.deltaTime;
        }
        

        if(!isAttacking && !isDamage && !dead)
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
        Gizmos.DrawWireCube(attackPos.position, new Vector2(attackRangeX, attackRangeY));
    }

    public void TakeDamage(int IncomingDamage)
    {
        isDamage = true;
        isAttacking = false;
        health -= IncomingDamage;
        //GameControl.health -= IncomingDamage;
        animator.SetBool("isDamaged", true);
        Debug.Log("Player damaged");
    }

    public void AlertObservers(string message)
    {
        if (message == "attackEnded")
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                Debug.Log("enemy found");
                if(comboCount == 3 || kick == true)
                {
                    enemiesToDamage[i].GetComponent<FrankMovement>().TakeDamage(damage * 2);
                    
                }
                else
                {
                    enemiesToDamage[i].GetComponent<FrankMovement>().TakeDamage(damage);
                }

                
                
            }
            if (comboCount == 3)
            {
                comboCount = 0;
                Debug.Log("Combo done");
            }
            isAttacking = false;

        }
    }


    public void DamagedObservers(string message)
    {
        if (message == "damageEnded")
        {
            
            animator.SetBool("isDamaged", false);
            isDamage = false;
        }
    }

    public void DeadObservers(string message)
    {
        if (message == "Dead")
        {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }
}