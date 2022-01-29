using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderOnCollisionEffect : MonoBehaviour
{
    // Start is called before the first frame update
    CaveSpiderController spider;
    private void Start()
    {
        spider = transform.parent.GetComponent<CaveSpiderController>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //add what happens if projectile hits the player here
            collision.gameObject.GetComponent<PlayerHealthScript>().takeDamage(spider.Damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //add what happens if projectile hits the player here
            other.gameObject.GetComponent<PlayerHealthScript>().takeDamage(spider.Damage);
        }
    }
}

