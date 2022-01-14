using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moenemies : GroundEnemyScript
{  // Start is called before the first frame update
    [SerializeField] public Animator anim;
    public LayerMask groundLayer, playerLayer;

    [Header("Movement variables")]
    public Vector3 walkPoint;
    public bool walkPointSet;

    [Header("Attack variables")]
    public float timeofLastAttack = 0;
    public bool isAttacking = false;
    public bool alreadyAttacking;
    [SerializeField]public float attackTimer;

    [Header("Animation Variables")]
    public string attack;
    public string die = "Die";

    [Header("States")]
    [SerializeField]public float detectionDistance;
    [SerializeField]public  float attackDistance;
    public bool Radius(float distance) => Physics.CheckSphere(this.gameObject.transform.position, distance, playerLayer);
    public bool playerDetected => Radius(detectionDistance);
    public bool playerInAttackRange => Radius(attackDistance);

    override public void Start()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        attackDistance = navMeshAgent.stoppingDistance;
        currentState = States.Patrolling;
    }

    // Update is called once per frame
    override public void Update()
    {
        switch (currentState)
        {
            case States.Patrolling:
                Patrolling();
                if (!playerInAttackRange && playerDetected) { currentState = States.Chasing; }
                break;
            case States.Chasing:
                Chase();
                if (playerInAttackRange && playerDetected) { currentState = States.Attacking; }
                break;
            case States.Attacking:
                Attacking();
                if (!playerInAttackRange && playerDetected) { currentState = States.Chasing; }
                break;
            default:
                //Enemy patrols if no player is detected
                if (!playerInAttackRange && !playerDetected) { currentState = States.Patrolling; }
                Die(die);
                break;
        }
    }
    virtual public void Patrolling()
    {
        //Search a walkpoint if there is none set yet
        if (!walkPointSet)
        {
            SearchRandomWalkPoint();
            MovementAnimation(false);
        }
        else
        {
            //Let the enemy walk towards the walkpoint only when the walkpoint is set
            navMeshAgent.SetDestination(walkPoint);
            //Enemy Looks at the target
            transform.LookAt(new Vector3(walkPoint.x, this.transform.position.y, walkPoint.z));
            MovementAnimation(true);
        }

        //Calculate distance to walkpoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached so set walkpoint set to false
        if (distanceToWalkPoint.magnitude <= navMeshAgent.stoppingDistance) walkPointSet = false;
    }
    virtual public void SearchRandomWalkPoint()
    {
        //Determine a random point in the Enemy's detection range 
        float randomZ = Random.Range(-detectionDistance / 2, detectionDistance / 2);
        float randomX = Random.Range(-detectionDistance / 2, detectionDistance / 2);

        walkPoint = new Vector3(this.gameObject.transform.position.x + randomX, this.gameObject.transform.position.y, this.gameObject.transform.position.y + randomZ);

        //Check if walkpoint is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer)) { walkPointSet = true; }
    }
    virtual public void Chase()
    {
        //Animation trigger
        MovementAnimation(true);

        //Enemy looks at player
        transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        //Enemy goes towards the player
        navMeshAgent.SetDestination(Player.transform.position);
    }
    virtual public void AnimationTrigger(string animation)
    {
        //Plays enemy animation
        anim.SetTrigger(animation);
    }
    virtual public void MovementAnimation(bool isMoving)
    {
        //changes movement variable based on bool
        int speedVariable = isMoving ? 1 : 0;
        anim.SetFloat("Speed", speedVariable);
    }
    virtual public void Attacking()
    {
        //The enemy aims at the player
        transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        float distanceToTarget = Vector3.Distance(Player.transform.position, transform.position);
        if (distanceToTarget <= attackDistance)
        {
            MovementAnimation(false);

            //Start the attack timer
            if (!isAttacking)
            {
                isAttacking = true;
                StartCoroutine(TimedAttack());
            }
        }
        else
        {
            isAttacking = false;
        }
    }

    virtual public IEnumerator TimedAttack()
    {
        yield return new WaitForSeconds(attackTimer);
        AnimationTrigger(attack);
        isAttacking = false;
    }
    virtual public void RandomAttackVariations(string attack1, string attack2)
    {
        int random = Random.Range(0, 2);
        if (random == 0) attack= attack1; else attack= attack2;
    }
    virtual public void Die(string animation)
    {
        if (Health <= 0)
        {
            AnimationTrigger(animation);
            Instantiate(Coin, transform.position + new Vector3(0, 1, 0), transform.rotation);
            Invoke(nameof(DeSpawn), 3f);
        }
    }
    public void DeSpawn()
    {
        Destroy(this.gameObject);
    }
    public void OnTriggerEnter(Collider collision)
    {
        //Projectile hurts enemy on collision
        if (collision.gameObject.tag == "Projectile")
        {
            AnimationTrigger("TakeDamage");
            TakeDamage(1);
        }

        //Enemy hurts player on collision
        if (collision.gameObject.tag == "Player")
        {
            //Player loses health
            Player.GetComponent<PlayerHealthScript>().takeDamage(Damage);
        }
    }
}
