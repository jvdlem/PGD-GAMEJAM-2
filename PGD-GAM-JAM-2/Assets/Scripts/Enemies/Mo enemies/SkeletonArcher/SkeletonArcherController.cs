using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcherController : Moenemies
{
    // Start is called before the first frame update
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject arrow;
    override public void Start()
    {
        die = "Die";
        attack = "Shoot";
    }
    override public void Attacking(string animation)
    {
        //The skeleton aims at the player
        transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        float distanceToTarget = Vector3.Distance(Player.transform.position, transform.position);
        if (distanceToTarget <= attackDistance)
        {
            MovementAnimation(false);

            //Start the attack timer
            if (!isAttacking)
            {
                isAttacking = true;
                timeofLastAttack = Time.time;
            }
            if (Time.time >= timeofLastAttack + attackTimer)
            {

                timeofLastAttack = Time.time;
                AnimationTrigger(animation);
                Shoot();
            }
        }
        else
        {
            isAttacking = false;
        }
    }
    void Shoot()
    {
            AnimationTrigger("Shoot");

            Rigidbody currentArrow = Instantiate(arrow, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            currentArrow.AddForce(transform.forward * 10f, ForceMode.Impulse);
            currentArrow.AddForce(transform.up * .25f, ForceMode.Impulse);
    }

}
