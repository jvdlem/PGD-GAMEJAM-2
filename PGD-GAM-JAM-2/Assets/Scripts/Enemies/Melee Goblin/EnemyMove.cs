using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : GroundEnemyScript
{
    float AttackTimer;
    int rushDistance;
    int rushSpeed;
    private bool playOnce;
    bool canAttack;

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
        navMeshAgent.speed = WalkSpeed;
        float dist = Vector3.Distance(Player.transform.position, this.transform.position);
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        base.Update();
        if (Health <= 0)
        {
            //Sound
            Instantiate(Coin, transform.position + new Vector3(0, 1, 0), transform.rotation);
            Destroy(this.gameObject);
        }

        AttackTimer += Time.deltaTime;
        if (AttackTimer > 2.5f && !canAttack && dist < rushDistance)
        {
            canAttack = true;
            AttackTimer = 0;
            Rush();
            if (playOnce)
            {
                //Sound
                playOnce = false;
            }
        }
        else
        {
            playOnce = true;
            WalkSpeed = 4;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            //Sound
            Health -= 1;
            Destroy(collision.gameObject);
        }


        if (collision.gameObject.tag == "Player")
        {
            if (canAttack)
            {
                Player.GetComponent<PlayerHealthScript>().takeDamage(1);
                canAttack = false;
            }
        }
    }

    private void Rush()
    {
        WalkSpeed = rushSpeed;
    }
}