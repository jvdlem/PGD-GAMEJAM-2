using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GolemScript golem;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && golem.canBeHurt)
        {
            //add what happens if projectile hits the player here
            collision.gameObject.GetComponent<PlayerHealthScript>().takeDamage(golem.Damage);
        }
    }

    private void Update()
    {
        Debug.Log(golem.canBeHurt);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && golem.canBeHurt)
        {
            //add what happens if projectile hits the player here
            other.gameObject.GetComponent<PlayerHealthScript>().takeDamage(golem.Damage);
        }
    }
}
