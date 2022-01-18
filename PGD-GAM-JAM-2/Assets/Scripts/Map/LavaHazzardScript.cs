using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHazzardScript : MonoBehaviour
{
    private float lavaTimer=0;
    // Start is called before the first frame update
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            lavaTimer += Time.deltaTime;
            if (lavaTimer < 1)
            {
                //add what happens if the player enters lava here
                //Burn Sound
                collision.gameObject.GetComponent<PlayerHealthScript>().takeDamage(1);
            }
            if (lavaTimer > 3) { lavaTimer = 0; }
        }
    }
}
