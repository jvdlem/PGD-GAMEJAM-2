using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public float lookAtRange = 50.0f;
    public float AttackRange = 20.0f;
    public float bulletSpeed = 20.0f;
    public float speed = 2.0f;

    public bool  projectileEnemyDeath = false;
    private bool inLookAtRange = false;
    private bool inAttackRange = false;


    public GameObject destroyedVersion;
    public Rigidbody projectile;
    void Start()
    {
        player = GameObject.Find("Player").transform;

        float rand = Random.Range(1.0f, 2.0f);
        InvokeRepeating("shoot", 2, rand);
    }
    void shoot() 
    {
        if (inAttackRange) 
        {

            
            Rigidbody bullet = (Rigidbody)Instantiate(projectile, transform.position + transform.forward, transform.rotation);
            bullet.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

            Destroy(bullet.gameObject, 2);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (projectileEnemyDeath) 
        {
            Destroy(gameObject);
            Instantiate(destroyedVersion, transform.position + new Vector3(0,-3,0), transform.rotation);
            
        }
        inAttackRange = Vector3.Distance(transform.position, player.position) < AttackRange;
        inLookAtRange = Vector3.Distance(transform.position, player.position) < lookAtRange;

        if (inAttackRange) { transform.LookAt(player); gameObject.GetComponent<Renderer>().material.color = Color.red; }
        else if (inLookAtRange) 
        { 
            transform.LookAt(player); gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            Debug.DrawLine(player.position, gameObject.transform.position, Color.yellow);
            gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
        }
        else gameObject.GetComponent<Renderer>().material.color = Color.green; ;

        
    }
}
