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
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            takeDamage(3);
        }
    }
    void takeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
