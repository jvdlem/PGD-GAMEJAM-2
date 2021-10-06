using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseScript : MonoBehaviour
{
    public int Health;
    public int Damage;
    public int Tier;
    private Rigidbody Rigidbody;
    public GameObject Player;

    void Start()
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

    void Update()
    {
        
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
