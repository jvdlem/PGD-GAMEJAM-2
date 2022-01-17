using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] GameObject Skeleton;
    private GameObject shootPoint;
    //  GameObject player;
    PlayerHealthScript playerHealth;
    Vector3 direction;
    //Arrow damage multiplier from Archer script
    private void Start()
    {
        shootPoint = GameObject.FindGameObjectWithTag("ArrowSpawnPoint");
        direction = Skeleton.transform.position - shootPoint.transform.position;


        //this.transform.rotation = Quaternion.
        //transform.LookAt(Player);
        playerHealth = GetComponent<PlayerHealthScript>();
        //Getcomponent Archer script
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Destroy bullet once it hits player
            playerHealth.takeDamage(1);
            Destroy(this.gameObject);
        }
        else
        {
            //Destroy bullet once it hits something else
            Destroy(this.gameObject);
        }

    }
    
}
