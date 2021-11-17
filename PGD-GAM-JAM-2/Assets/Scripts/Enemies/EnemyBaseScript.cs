using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseScript : MonoBehaviour
{
    public int Health;
    public int Damage;
    public int Tier;
    protected Rigidbody Rigidbody;
    public GameObject Player;

    protected Vector3 velocity; //Velocity for movement

    private enum States
    {
        Attacking,
        Patrolling,
        Chasing,
        Death
    }

    private States currentState;

    public virtual void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        //Gets Rigidbody of gameobject, if null, create and add new.
        Rigidbody = GetComponent<Rigidbody>();
        if (Rigidbody == null)
        {
            Rigidbody = gameObject.AddComponent<Rigidbody>();
        }

        //Multiply healt and damage if the enemy is a higher tier
        if (Tier > 1)
        {
            Health *= Tier;
            Damage *= Tier;
        }
    }

    public virtual void Update()
    {

        transform.position += velocity; //Velocity tied to position

        switch (currentState)
        {
            case States.Attacking:
                break;
            case States.Patrolling:
                break;
            case States.Chasing:
                break;
            case States.Death:
                break;
            default:
                break;
        }
    }

    public void TakeDamage(int pDamage)
    {
        Health -= pDamage;
        if (Health <= 0)
        {
            //death
        }
    }
}
