using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : GroundEnemyScript
{
    float AttackTimer, deathTimer;
    int rushDistance;
    int rushSpeed;
    private bool playOnce;
    bool canAttack;
    Animator gobboAnimation;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Health = 10;
        Tier = 1;
        checkForPlayerDistance = 20;
        WalkSpeed = 4;
        RotateSpeed = 8;
        AttackRange = 2.2f;
        rushDistance = 5;
        rushSpeed = 8;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Health <= 0)
        {
            if (playOnce)
            {
                //FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Goblin/GoblinDeath", this.gameObject.transform.position);
                playOnce = false;
            }
            gobboAnimation.Play("ms05_04_Die");   
            WalkSpeed = 0; //stop the gobbo from walking.

            deathTimer += Time.deltaTime; //timer for removing gobbo after animation.
            if (deathTimer >= 2f) {
                Instantiate(Coin, transform.position + new Vector3(0, 1, 0), transform.rotation); //spawns coins.
                Destroy(this.gameObject); //remove gobbo.
            }
        }
        else
        {
            navMeshAgent.speed = WalkSpeed;
            float dist = Vector3.Distance(Player.transform.position, this.transform.position);
            this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
            playOnce = true;
            
            if (dist >= checkForPlayerDistance)
            {
                gobboAnimation.Play("ms05_04_Walk");
            } else if(dist < rushDistance)
            {
                Rush();
            }

            AttackTimer += Time.deltaTime;
            if (AttackTimer > 2.5f && dist < rushDistance)
            {
                gobboAnimation.Play("ms05_04_Attack_01");
                AttackTimer = 0;
                if (playOnce)
                {
                    //FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Goblin/GoblinWindup", this.gameObject.transform.position);
                    playOnce = false;
                }
            }
            else
            {
                playOnce = true;
                WalkSpeed = 4;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Goblin/GoblinHurt", this.gameObject.transform.position);
            Health -= 1;
            Destroy(collision.gameObject);
        }


        if (collision.gameObject.tag == "Player")
        {
            Player.GetComponent<PlayerHealthScript>().takeDamage(1);
        }
    }
    private void Rush()
    {
        WalkSpeed = rushSpeed;
        gobboAnimation.Play("ms05_04_Run");
    }
}