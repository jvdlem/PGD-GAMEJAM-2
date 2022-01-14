using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    //[SerializeField] GameObject Skeleton;
   // [SerializeField] Transform shootPoint;
  //  GameObject player;
    PlayerHealthScript playerHealth;
    //Arrow damage multiplier from Archer script
    private void Start()
    {
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
