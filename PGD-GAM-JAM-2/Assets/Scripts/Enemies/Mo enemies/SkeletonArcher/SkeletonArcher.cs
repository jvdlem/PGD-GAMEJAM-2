using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcher : GroundEnemyScript
{
    // Start is called before the first frame update
    [SerializeField] public Animator anim;
    public LayerMask groundLayer, playerLayer;

    [Header("Movement variables")]
    public Vector3 walkPoint;
    bool walkPointSet;

    [Header("Attack variables")]
    [SerializeField] Transform shootPoint;
    public float timeBetweenAttacks;
    bool isAttacking;
    public GameObject arrow;

    [Header("States")]
    [SerializeField]public float detectionDistance;
    [SerializeField]public float attackDistance;
    private bool Radius(float distance) => Physics.CheckSphere(this.gameObject.transform.position, distance, playerLayer);
    public bool playerDetected => Radius(detectionDistance);
    public bool playerInAttackRange => Radius(attackDistance);

    override public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        currentState = States.Patrolling;
        InvokeRepeating(nameof(Shoot), timeBetweenAttacks, timeBetweenAttacks);
    }
    // Update is called once per frame
    override public void Update()
    {
        Debug.Log(currentState);
        Debug.Log("In attack range"+playerInAttackRange);
        Debug.Log("Detected"+playerDetected);

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
                Aiming();
                if (!playerInAttackRange && playerDetected) { currentState = States.Chasing; }
                break;
            case States.Death:
                break;
            default:
                if (!playerInAttackRange && !playerDetected) { currentState = States.Patrolling; }
                Die();
                break;
        }
    }
    private void AnimationTrigger(string animation)
    {
        //Triggers different animations
        anim.SetTrigger(animation);
    }
    private void MovementAnimation(bool isMoving)
    {
        //Changes the vaulue of the speed variable based on a bool
        int speedVariable = isMoving ? 1 : 0;
        anim.SetFloat("Speed", speedVariable);
    }
    private void Patrolling()
    {
        //Search a walkpoint if there is none set yet
        if (!walkPointSet)
        {
            SearchRandomWalkPoint();
            MovementAnimation(false);
        }

        //Let the golem walk towards the walkpoint only when the walkpoint is set
        if (walkPointSet)
        {
            navMeshAgent.SetDestination(walkPoint);
            //Golem Looks at the target
            transform.LookAt(new Vector3(walkPoint.x, this.transform.position.y, walkPoint.z));
            MovementAnimation(true);
        }

        //Calculate distance to walkpoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached so set walkpoint set to false
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }
    private void SearchRandomWalkPoint()
    {
        //Determine a random point in the Golems detection range 
        float randomZ = Random.Range(-detectionDistance / 2, detectionDistance / 2);
        float randomX = Random.Range(-detectionDistance / 2, detectionDistance / 2);

        walkPoint = new Vector3(this.gameObject.transform.position.x + randomX, this.gameObject.transform.position.y, this.gameObject.transform.position.y + randomZ);

        //Check if walkpoint is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer)) { walkPointSet = true; }
    }
    private void Chase()
    {
        MovementAnimation(true);

        //Golem looks at player and goes to their position
        transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
        navMeshAgent.SetDestination(Player.transform.position);
    }
    private void Aiming()
    {
        //The golem stands stil during attack
        MovementAnimation(false);
        navMeshAgent.SetDestination(transform.position);

        //The golem aims at the player
        transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
    }
    void Shoot()
    {
        if (currentState == States.Attacking)
        {
            AnimationTrigger("Shoot");

            Rigidbody currentArrow = Instantiate(arrow, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            currentArrow.AddForce(transform.forward * 10f, ForceMode.Impulse);
            currentArrow.AddForce(transform.up * .25f, ForceMode.Impulse);
        }
    }
    void Die()
    {
        if (Health <= 0)
        {
            AnimationTrigger("Die");
            Instantiate(Coin, transform.position + new Vector3(0, 1, 0), transform.rotation);
            Invoke(nameof(DeSpawn), 3f);
        }
    }
    private void DeSpawn()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider collision)
    {
        //Projectile hurts skeleton on collision
        if (collision.gameObject.tag == "Projectile")
        {
            Health--;
        }

        //Skeleton hurts player on collision
        if (collision.gameObject.tag == "Player")
        {
            //Player loses health
            Player.GetComponent<PlayerHealthScript>().takeDamage(3);
        }
    }
}


