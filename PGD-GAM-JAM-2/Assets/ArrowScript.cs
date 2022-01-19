using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //add what happens if projectile hits the player here
            //Sound
            //Destroy bullet once it hits something else
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerHealthScript>().takeDamage(1);
        }
        else Destroy(gameObject);
    }
}
