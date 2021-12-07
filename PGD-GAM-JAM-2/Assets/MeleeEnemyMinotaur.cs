using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyMinotaur : GroundEnemyScript
{
    int outerRange, middleRange, innerRange;
    bool backOffTargetSet;
    Vector3 backOffTarget = new Vector3();

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Health = 20;
        Tier = 1;
        checkForPlayerDistance = 30;
        WalkSpeed = 6;
        RotateSpeed = 8;
        AttackRange = 0f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }

        navMeshAgent.speed = WalkSpeed;
        float dist = Vector3.Distance(Player.transform.position, this.transform.position);
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        if(dist < innerRange)
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Health -= 1;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
                Player.GetComponent<PlayerHealthScript>().takeDamage(1);
        }
    }
    void Retreat()
    {
        //Make the hound look at the player
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        if (!backOffTargetSet)
        {
            SearchBackOffTarget();
        }
        if (backOffTargetSet)
        {
            //Move to code 
            navMeshAgent.SetDestination(backOffTarget);
        }
        //Calculate distance to pounce target
        Vector3 distanceToBackOffPoint = transform.position - backOffTarget;

        //Target reached
        if (distanceToBackOffPoint.magnitude < 1f)
        {
            //Effect of reaching the backoff point 
            //currentState = States.Hopping;
        }
    }

    void SearchBackOffTarget()
    {
        //Back off distance
        float backOffDistance = -1;

        //Set target coordinates from current position
        backOffTarget = new Vector3(transform.position.x, transform.position.y, transform.position.z + backOffDistance);

        //Set target bool true if the hop target is on the ground
        //if (Physics.Raycast(backOffTarget, -transform.up, 2f, groundLayer)) backOffTargetSet = true;
    }
}
