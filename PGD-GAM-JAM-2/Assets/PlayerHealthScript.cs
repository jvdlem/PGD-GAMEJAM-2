using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/MISC/ElevatorMusic", this.transform.gameObject);
    }


    void Update()
    {
        MaxHealth();
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            takeDamage(3);
        }

    }

    void MaxHealth() 
    {
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;

        }
        
        
        

    }

    void takeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
