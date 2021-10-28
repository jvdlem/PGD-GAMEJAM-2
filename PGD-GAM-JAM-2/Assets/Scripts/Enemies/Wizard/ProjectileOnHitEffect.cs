using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOnHitEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            
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
