using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    //  GameObject player;
    PlayerHealthScript playerHealth;
    //Arrow damage multiplier from Archer script
    private void Start()
    {
        playerHealth = GetComponent<PlayerHealthScript>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Destroy bullet once it hits player
            playerHealth.takeDamage(1);
            Destroy(this.gameObject);

            Debug.Log("HIT");
        }

        //Destroy bullet once it hits something else
        Destroy(this.gameObject);
    }
}
