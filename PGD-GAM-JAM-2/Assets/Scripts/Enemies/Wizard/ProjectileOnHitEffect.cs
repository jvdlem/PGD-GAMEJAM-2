using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOnHitEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            //add what happens if projectile hits the player here
            FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Wizard/FireBallExplosion", this.gameObject.transform.position);
            
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerHealthScript>().takeDamage(3);
        }
        else Destroy(gameObject);
    }
   
}
