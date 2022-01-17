using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : GroundEnemyScript
{
    float AttackTimer, IdleTimer, DeathTimer, retreatDistance = 2f, attackDistance = 2.5f;
    int rushDistance = 10, rushSpeed = 14, idleSpeed = 0, walkSpeed = 8, Speed;
    private bool playOnce;
    bool ded = false;
    bool gotRetreatTarget, attemptAttack;
    Vector3 retreatTarget = Vector3.zero;
    public LayerMask groundLayer;
    [SerializeField] Animator anim;
    string attackSound, deathSound, hurtSound, windupSound, idleSound;
    enum States
    {
        Idle,
        Following,
        Rushing,
        Attacking,
        Retreating,
        Death
    }
    private States currentGoblinState;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        currentGoblinState = States.Idle;
        Health = 10;
        Tier = 1;
        checkForPlayerDistance = 20;
        //WalkSpeed = 4;
        RotateSpeed = 8;
        AttackRange = 0.5f;
        rushDistance = 5;
        rushSpeed = 8;
        //Initialise Sounds
        attackSound = "event:/Enemy/Goblin/GoblinAttack";
        deathSound = "event:/Enemy/Goblin/GoblinDeath";
        hurtSound = "event:/Enemy/Goblin/GoblinHurt";
        windupSound = "event:/Enemy/Goblin/GoblinWindup";
        idleSound = "event:/Enemy/Goblin/GoblinIdle";
    }

    // Update is called once per frame
    public override void Update()
    {
        navMeshAgent.speed = Speed;
        float dist = Vector3.Distance(Player.transform.position, this.transform.position);
        base.Update();
        if (Health <= 0) currentGoblinState = States.Death;
        else if (dist > checkForPlayerDistance) currentGoblinState = States.Idle;
        else if (dist <= checkForPlayerDistance && dist > rushDistance) currentGoblinState = States.Following;
        else if (dist <= rushDistance && dist > attackDistance) currentGoblinState = States.Rushing;
        else if (dist <= attackDistance) currentGoblinState = States.Attacking;
        //else if (dist < retreatDistance) currentState = States.Retreating;

        switch (currentGoblinState)
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

        if (collision.gameObject.tag == "Projectile" && !ded)
        {
            PlaySound(hurtSound, this.gameObject.transform.position);
            int dmg = (int)collision.gameObject.GetComponent<Projectille>().dmg;
            Health -= dmg;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile" && !ded)
        {
            PlaySound(hurtSound, this.gameObject.transform.position);
            int dmg = (int)collision.gameObject.GetComponent<Projectille>().dmg;
            Health -= dmg;
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
            currentGoblinState = States.Following;
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
        ded = true;
        anim.Play("Die");
        Speed = idleSpeed;
        navMeshAgent.SetDestination(this.transform.position);
        PlaySound(deathSound, this.gameObject.transform.position);
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
            PlaySound(idleSound, this.gameObject.transform.position);
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
            PlaySound(attackSound, this.gameObject.transform.position);
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
        anim.Play("Run");
        PlaySound(windupSound, this.gameObject.transform.position);
    }

    private void PlaySound(string sound, Vector3 pos)
    {
        FMODUnity.RuntimeManager.PlayOneShot(sound, pos);
    }
}