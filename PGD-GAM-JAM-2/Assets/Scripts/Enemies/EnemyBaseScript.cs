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
    private GameObject[] Players;
    public GameObject Coin;
    protected float animationTime = 1;
    private bool canDie = true;

    protected Vector3 velocity; //Velocity for movement

    //Various enemy states
    protected enum States
    {
        Attacking,
        Patrolling,
        Chasing,
        Hurt,
        Death
    }

    //Current state of enemy
    protected States currentState;

    public virtual void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject PlayerObject in Players)
        {
            print(PlayerObject);
            if (PlayerObject.activeSelf == true)
            {
                Player = PlayerObject;
            }
        }

        //Gets Rigidbody of gameobject, if null, create and add new.
        Rigidbody = GetComponent<Rigidbody>();
        if (Rigidbody == null)
        {
            Rigidbody = gameObject.AddComponent<Rigidbody>();
        }

        //Multiply health and damage if the enemy is a higher tier
        if (Tier > 1)
        {
            Health *= Tier;
            Damage *= Tier;
        }
    }

    public virtual void Update()
    {
        transform.position += velocity * Time.deltaTime; //Velocity tied to position
    }

    public void TakeDamage(int pDamage)
    {
        Health -= pDamage;

        if (Health <= 0 && canDie)
        {
            this.
            canDie = false;
            DIE();
            //death
        }
    }

    IEnumerator DIE()
    {
        
        this.Rigidbody.isKinematic = true;
        yield return new WaitForSeconds(animationTime);
        Instantiate(Coin, transform.position + new Vector3(0, 1, 0), transform.rotation);
        Destroy(this.gameObject);
    }

    

}
