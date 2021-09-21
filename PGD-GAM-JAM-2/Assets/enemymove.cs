using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] GameObject player;

    float speed = 8; //speed of the goblin.
    int trackDistance = 20; //tracking distance of the goblin vs the player.

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Sphere");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        float step = speed * Time.deltaTime; // calculate distance to move

        if (dist < trackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            transform.LookAt(player.transform);
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "MeleeEnemy")
        //{
        //    transform.position = Vector3.up * 50;
        //    Destroy(this);
        //}
    }
}
