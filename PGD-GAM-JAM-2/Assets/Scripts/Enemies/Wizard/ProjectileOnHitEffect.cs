using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOnHitEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            //add what happens if projectile hits the player here
            player.GetComponent<PlayerHealthScript>().takeDamage(3);
            Destroy(gameObject);
        }
        else Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
