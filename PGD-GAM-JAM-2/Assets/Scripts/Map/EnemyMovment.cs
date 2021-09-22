using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    private GameObject player;
    private Rigidbody myBody;
    private Vector3 force;
    private float health = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Playboy");
        myBody = this.gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    public float movementSpeed = 5f; //for instance

    void Update()
    {

        Vector3 direction = (player.transform.position - transform.position).normalized;
        myBody.MovePosition(transform.position + direction * movementSpeed * Time.deltaTime);
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }


    }

    public void Getdamage(float dmg)
    {
        health -= dmg;
    }
}
