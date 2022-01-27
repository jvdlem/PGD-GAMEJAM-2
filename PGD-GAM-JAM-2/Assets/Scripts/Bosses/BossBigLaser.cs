using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBigLaser : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //add what happens if projectile hits the player here
            //Sound

            //Destroy(gameObject);
            other.gameObject.GetComponent<PlayerHealthScript>().takeDamage(1);
        }
    }
}
