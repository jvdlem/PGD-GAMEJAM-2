using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HellHoundScript : GroundEnemyScript
{
    //[SerializeField] public Animator anim;
    public LayerMask groundLayer, playerLayer;
    //[SerializeField] public Transform target;
    private Transform hound;
    private PlayerHealthScript playerHealth;

    //Attack variables
    public bool isAttacking = false, pounceTargetSet = false;
    public int attackDamage = 2;
    private float attackTimer = .5f;
    private float pounceDistance = 1;
    public Vector3 pounceTarget = Vector3.zero;
    public float attackDistance = 2;

    //Move variables
    private float speed, hopHeight;
    private bool isGrounded = true;
    public Vector3 walkPoint;
    public Vector3 velocity = Vector3.zero;
    public static float rotateSpeed = 3f;
    public Vector3 futurePosition;
    public bool walkPointSet = false, hopTargetSet = false, backOffTargetSet = false;
    public int idleTimer = 1;
    private float hopDistance = 1;
    public Vector3 hopTarget = Vector3.zero, backOffTarget = Vector3.zero;

    //States variables
    public float detectionDistance = 10;
    public bool playerDetected, playerInAttackRange, alreadyAttacking;
    enum States
    {
        Attacking,
        Chasing,
        Patrolling,
        Hopping,
        BackingOff,
        Death
    }
    private States currentState;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = GetComponent<PlayerHealthScript>();
       // anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = States.Patrolling;
        hound = GetComponent<Transform>();
        Health = 5;
        Tier = 1;
    }
    // Update is called once per frame
    void Update()
    {
        playerDetected = Physics.CheckSphere(transform.position, detectionDistance, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackDistance, playerLayer);

        if (!playerDetected && !playerInAttackRange) { currentState = States.Patrolling; }
        if (playerDetected && !playerInAttackRange) { currentState = States.Chasing; }

        //Death statement
        if (Health <= 0)
        {
            Instantiate(Coin, transform.position + new Vector3(0, 1, 0), transform.rotation);
            Destroy(this.gameObject);
        }

        switch (currentState)
        {
            case States.Patrolling:
                hopTargetSet = false;
                pounceTargetSet = false;
                backOffTargetSet = false;
                Patrolling();
                break;
            case States.Chasing:
                hopTargetSet = false;
                pounceTargetSet = false;
                backOffTargetSet = false;

                Chasing();

                if (playerDetected && playerInAttackRange) { currentState = States.Attacking; }
                break;
            case States.Attacking:
                //Forget previous hop target
                hopTargetSet = false;
                backOffTargetSet = false;
                Pounce();
                break;
            case States.BackingOff:
                pounceTargetSet = false;
                hopTargetSet = false;
                JumpBack();
                break;
            case States.Hopping:
                //Erase the previous target position
                backOffTargetSet = false;
                pounceTargetSet = false;
                Hop();
                break;
            default:
                break;
        }
        Debug.Log(currentState);
    }
    void Chasing()
    {
        //chases while looking at the player
        hound.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        //go to the target
        navMeshAgent.SetDestination(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
    }
    void Pounce()
    {
        //Make the hound look at the player
        hound.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        if (!pounceTargetSet)
        {
            //Isn't attacking
            isAttacking = false;

            //Reset the hound speed
            //navMeshAgent.speed = 7;

            SearchPounceTarget();
        }
        if (pounceTargetSet)
        {
            //Hound attacks
            isAttacking = true;

            //MoveToTargetDirection(pounceTarget,7);

            ////The hound pounces towards target fast
            //navMeshAgent.speed = 10;
            navMeshAgent.SetDestination(pounceTarget);

            //Play animation
            //anim.Play("Pounce");
        }
        //Calculate distance to pounce target
        Vector3 distanceToPouncePoint = transform.position - pounceTarget;

        //Pounce target reached
        if (distanceToPouncePoint.magnitude < 2f)
        {
            //Back off after attack
            currentState = States.BackingOff;
        }
    }
    void JumpBack()
    {
        //Make the hound look at the player
        hound.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        if (!backOffTargetSet)
        {
            SearchBackOffTarget();
        }
        if (backOffTargetSet)
        {
            //Play animation
            //anim.Play("Hop");

            //Move to code 
            //MoveToTargetDirection(backOffTarget,2);
            navMeshAgent.SetDestination(backOffTarget);
        }
        //Calculate distance to pounce target
        Vector3 distanceToBackOffPoint = transform.position - backOffTarget;

        //Pounce target reached
        if (distanceToBackOffPoint.magnitude < 1f)
        {
            //Effect of reaching the backoff point 
            currentState = States.Hopping;
        }
    }
    void MoveToTargetDirection(Vector3 target, float speed)
    {
        Vector3 direction = target - transform.position;
        transform.position += direction * Time.deltaTime; //* speed;
    }
    void SearchBackOffTarget()
    {
        float backOffDistance = -1;

        //Set target coordinates from current position
        backOffTarget = new Vector3(transform.position.x, transform.position.y, transform.position.z + backOffDistance);

        //Set target bool true if the hop target is on the ground
        if (Physics.Raycast(backOffTarget, -transform.up, 2f, groundLayer)) backOffTargetSet = true;
    }
    void Hop()
    {
        //hops sideways while looking at the player
        hound.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        if (!hopTargetSet)
        {
            SearchHopTarget();
        }
        if (hopTargetSet)
        {
            //Set hop speed
            //navMeshAgent.speed = 10;

            //Play animation
            //anim.Play("Hop");


            //Movement 
            navMeshAgent.SetDestination(hopTarget);

            //MoveToTargetDirection(hopTarget,2);
        }
        //Calculate distance to pounce target
        Vector3 distanceToHopPoint = transform.position - hopTarget;

        //Hop target reached
        if (distanceToHopPoint.magnitude < 1f)
        {
            //Reset hound speed
            //navMeshAgent.speed = 7;

           // anim.Play("IdleHound");

            //go to a random State after the hop
            float randomState = Random.Range(0, 2);
            if (randomState == 0)
            {
                //Forget previous hop target
                hopTargetSet = false;
            }
            else currentState = States.Attacking;
        }
    }
    private void Patrolling()
    {
        //Search a walkpoint if there is none set yet
        if (!walkPointSet)
        {
            SearchRandomWalkPoint();
            //anim.Play("IdleHound");
        }

        //Let the golem walk towards the walkpoint only when the walkpoint is set
        if (walkPointSet)
        {
            navMeshAgent.SetDestination(walkPoint);
            //anim.Play("Walking");
        }

        //Calculate distance to walkpoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached so set walkpoint set to false
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }
    private void SearchRandomWalkPoint()
    {
        //Determine a random point in the Golems detection range 
        float randomZ = Random.Range(-detectionDistance / 4, detectionDistance / 4);
        float randomX = Random.Range(-detectionDistance / 4, detectionDistance / 4);

        walkPoint = new Vector3(this.gameObject.transform.position.x + randomX, this.gameObject.transform.position.y, this.gameObject.transform.position.y + randomZ);

        //Check if walkpoint is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer)) { walkPointSet = true; }
    }
    void SearchPounceTarget()
    {
        //the hound aims for the player
        //hound.transform.LookAt(target.position);

        //Set new target from current position
        pounceTarget = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);

        //Set target bool true if the target is on the ground 
        if (Physics.Raycast(pounceTarget, -transform.up, 2f, groundLayer)) pounceTargetSet = true;
    }
    void SearchHopTarget()
    {
        //Randomize a direction to hop towards from the current position
        float hopdirection;
        float randomX = Random.Range(0, 1);
        if (randomX == 0)
        {
            hopdirection = 1;
        }
        else hopdirection = -1;

        //Set target coordinates from current position
        hopTarget = new Vector3(transform.position.x + hopdirection, transform.position.y, transform.position.z);

        //Set target bool true if the hop target is on the ground
        if (Physics.Raycast(hopTarget, -transform.up, 2f, groundLayer)) hopTargetSet = true;
    }
    private void OnTriggerEnter(Collider collision)
    {
        //Projectile hurts Hound on collision
        if (collision.gameObject.tag == "Projectile")
        {
            Health--;
        }

        //Hound hurts player on collision
        if (isAttacking)
        {
            if (collision.gameObject.tag == "Player")
            {
                //Player loses health
                Player.GetComponent<PlayerHealthScript>().takeDamage(3);
            }
        }
    }
}