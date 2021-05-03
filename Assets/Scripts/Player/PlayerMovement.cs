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
    private AudioSource sound;
    public AudioClip death;
    public AudioClip walking;
    public AudioClip punch;
    public AudioClip powerup;
    private bool kick;
    public int DamageMulit;
    private float boostTimerS;
    private float boostTimerD;
    public bool speedBoosting;
    public bool damageBoosting;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Health") > 0 || health != 5)
        {
            health = PlayerPrefs.GetInt("Health");
        }
        isDamage = false;
        dead = false;
        comboCount = 0;
        DamageMulit = 1;
        

 
        
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
        PlayerPrefs.SetInt("Health", health);

        if (health > 5)
        {
            health = 5;
        }

        if (health < 1)
        {
            animator.SetBool("isDead", true);
            sound = GetComponent<AudioSource>();
            sound.PlayOneShot(death);
            dead = true;

        }

        if (speedBoosting)
        {
            boostTimerS += Time.deltaTime;
            if (boostTimerS >= 10)
            {
                Speed = 10;
                boostTimerS = 0;
                speedBoosting = false;
                GameObject.Find("GameControl").GetComponent<GameControl>().SB = false;
            }
        }

        if (damageBoosting)
        {
            boostTimerD += Time.deltaTime;
            if (boostTimerD >= 10)
            {
                DamageMulit = 1;
                boostTimerD = 0;
                damageBoosting = false;
                GameObject.Find("GameControl").GetComponent<GameControl>().DB = false;
            }
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
        animator.SetBool("isDamaged", true);
        Debug.Log("Player damaged");
    }

    public void AlertObservers(string message)
    {
        if (message == "attackEnded")
        {
            sound = GetComponent<AudioSource>();
            sound.volume = 0.1f;
            sound.PlayOneShot(punch);
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                Debug.Log("enemy found");
                if(comboCount == 3 || kick == true)
                {
                    enemiesToDamage[i].GetComponent<FrankMovement>().TakeDamage(damage * 2 * DamageMulit);
                    
                }
                else
                {
                    enemiesToDamage[i].GetComponent<FrankMovement>().TakeDamage(damage * DamageMulit);
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

    public void walkObservers(string message)
    {
        sound = GetComponent<AudioSource>();
        sound.volume = 0.1f;
        sound.PlayOneShot(walking);
    }

    public void powerPick()
    {
        sound = GetComponent<AudioSource>();
        sound.volume = 0.1f;
        sound.PlayOneShot(powerup);
    }
}