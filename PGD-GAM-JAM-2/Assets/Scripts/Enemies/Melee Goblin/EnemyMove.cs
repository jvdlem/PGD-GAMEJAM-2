using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : GroundEnemyScript
{
    float AttackTimer, IdleTimer, DeathTimer, retreatDistance = 2f, attackDistance = 2.5f;
    int rushDistance = 10, rushSpeed = 14, idleSpeed = 0, walkSpeed = 8, Speed;
    private bool playOnce;
    bool gotRetreatTarget, attemptAttack;
    Vector3 retreatTarget = Vector3.zero;
    public LayerMask groundLayer;
    Vector3 pos;
    [SerializeField] Animator anim;
    enum States
    {
        Idle,
        Following,
        Rushing,
        Attacking,
        Retreating,
        Death
    }
    private States currentState;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        currentState = States.Idle;
        Health = 10;
        Tier = 1;
        checkForPlayerDistance = 20;
        //WalkSpeed = 4;
        RotateSpeed = 8;
        AttackRange = 0.5f;
        rushDistance = 5;
        rushSpeed = 8;
    }

    // Update is called once per frame
    public override void Update()
    {
        pos = this.gameObject.transform.position;
        navMeshAgent.speed = Speed;
        float dist = Vector3.Distance(Player.transform.position, this.transform.position);
        base.Update();
        if (Health <= 0) currentState = States.Death;
        else if (dist > checkForPlayerDistance) currentState = States.Idle;
        else if (dist <= checkForPlayerDistance && dist > rushDistance) currentState = States.Following;
        else if (dist <= rushDistance && dist > attackDistance) currentState = States.Rushing;
        else if (dist <= attackDistance) currentState = States.Attacking;
        //else if (dist < retreatDistance) currentState = States.Retreating;

        switch (currentState)
        {
            case States.Idle:
                Idle();
                break;
            case States.Following:
                Following();
                break;
            case States.Rushing:
                Rush();
                break;
            case States.Attacking:
                Attacking();
                break;
            case States.Retreating:
                Retreat();
                break;
            case States.Death:
                Death();
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (attemptAttack)
        {
            if (collision.gameObject.tag == "Player")
            {
                Player.GetComponent<PlayerHealthScript>().takeDamage(1);
                attemptAttack = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Goblin/GoblinHurt", pos);
            Health -= 1;
            Destroy(collision.gameObject);
        }
    }

        void Retreat()
    {
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
        Speed = walkSpeed;
        anim.Play("Walk");

        if (!gotRetreatTarget)
        {
            SearchRetreatTarget();
        }
        if (gotRetreatTarget)
        {
            navMeshAgent.SetDestination(retreatTarget);
        }

        Vector3 distanceToBackOffPoint = transform.position - retreatTarget;

        if (distanceToBackOffPoint.magnitude < 1f)
        {
            currentState = States.Following;
        }
    }
    void SearchRetreatTarget()
    {
        float backOffDistance = -1;

        retreatTarget = new Vector3(transform.position.x, transform.position.y, transform.position.z + backOffDistance);

        if (Physics.Raycast(retreatTarget, -transform.up, 2f, groundLayer)) gotRetreatTarget = true;
    }

    void Death()
    {
        anim.Play("Die");
        Speed = idleSpeed;
        navMeshAgent.SetDestination(this.transform.position);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Goblin/GoblinDeath", pos);
        DeathTimer += Time.deltaTime;
        if (DeathTimer > 1.65f)
        {
            DeathTimer = 0;
            Instantiate(Coin, transform.position + new Vector3(0, 1, 0), transform.rotation);
            Destroy(this.gameObject);
        }
    }

    void Idle()
    {
        Speed = idleSpeed;
        anim.Play("Idle");
        IdleTimer += Time.deltaTime;
        if (IdleTimer > 8)
        {
            IdleTimer = 0;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Goblin/GoblinIdle", pos);
        }
    }

    void Attacking()
    {
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
        Speed = idleSpeed;

        AttackTimer += Time.deltaTime;
        if (AttackTimer > 1.5f)
        {
            anim.Play("Attack_01");
            FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Goblin/GoblinAttack", pos);
            attemptAttack = true;
            AttackTimer = 0;
        }
    }

    void Following()
    {
        Speed = walkSpeed;
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
        anim.Play("Walk");
    }

    private void Rush()
    {
        Speed = rushSpeed;
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
        FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Goblin/GoblinWindup", pos);
        anim.Play("Run");
    }
}