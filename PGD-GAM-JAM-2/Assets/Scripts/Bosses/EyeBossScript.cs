using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBossScript : EnemyBaseScript
{
    //The Boss parent scipts
    private BossScript parentBoss;
    public new Renderer renderer;

    //Default speed of the eyes tracking the player
    public float lookSpeed = 3f;

    //Bool if the eye is acive or not
    public bool EyeIsActive;

    override public void Start()
    {
        //Gets the components needed and fills them
        parentBoss = transform.parent.GetComponent<BossScript>();
        renderer = GetComponent<Renderer>();

        Player = parentBoss.Player;
    }

    override public void Update()
    {
        //Look towards player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z)), lookSpeed * Time.deltaTime);

        //Change color depending on if eye is active
        if (EyeIsActive)
        {
            renderer.material.color = Color.white;
        }
        else
        {
            renderer.material.color = Color.black;
        }
    }

    //Collision detection if eye is being hit by player projectile
    private void OnCollisionEnter(Collision collision)
    {
        //Check if eye that gets hit is active
        if (EyeIsActive)
        {
            if (collision.gameObject.tag == "Projectile")
            {
                //Check if projectile that hit has the "Projectille" base class
                if (collision.gameObject.GetComponent<Projectille>())
                {
                    //Deal x amount of damage to the boss parent, x being the damage the projectile does
                    parentBoss.BossCurrentHealth -= (int)collision.gameObject.GetComponent<Projectille>().dmg;

                    //Check if boss goes under the next health trigger, if yes set bool CycleToNextEye in parent boss scipt to true
                    if (parentBoss.BossCurrentHealth <= parentBoss.NextHealthTrigger)
                    {
                        parentBoss.BossCurrentHealth = parentBoss.NextHealthTrigger;
                        parentBoss.CycleToNextEye = true;
                    }

                    //If the boss hits 0 HP or goes under, go to the Die State
                    if (parentBoss.BossCurrentHealth <= 0)
                    {
                        parentBoss.CurrentBossState = BossScript.BossStates.DieState;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if eye that gets hit is active
        if (EyeIsActive)
        {
            if (other.gameObject.tag == "Projectile")
            {
                //Check if projectile that hit has the "Projectille" base class
                if (other.gameObject.GetComponent<Projectille>())
                {
                    //Deal x amount of damage to the boss parent, x being the damage the projectile does
                    parentBoss.BossCurrentHealth -= (int)other.gameObject.GetComponent<Projectille>().dmg;

                    //Check if boss goes under the next health trigger, if yes set bool CycleToNextEye in parent boss scipt to true
                    if (parentBoss.BossCurrentHealth <= parentBoss.NextHealthTrigger)
                    {
                        parentBoss.BossCurrentHealth = parentBoss.NextHealthTrigger;
                        parentBoss.CycleToNextEye = true;
                    }

                    //If the boss hits 0 HP or goes under, go to the Die State
                    if (parentBoss.BossCurrentHealth <= 0)
                    {
                        parentBoss.CurrentBossState = BossScript.BossStates.DieState;
                    }
                }
            }
        }
    }
}
