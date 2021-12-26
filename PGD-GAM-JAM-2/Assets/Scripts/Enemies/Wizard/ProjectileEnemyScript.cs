using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    public GameObject Coin;
    private float health = 10;
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
        float rand = Random.Range(1.0f, 2.0f);
        InvokeRepeating("shoot", 2, rand);
        player = GameObject.FindGameObjectWithTag("Player");


    }
    void shoot() 
    {
        if (inAttackRange) 
        {
            //Sound
            Rigidbody bullet = (Rigidbody)Instantiate(projectile, transform.position + transform.forward, transform.rotation);
            bullet.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

            Destroy(bullet.gameObject, 2);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            //Sound
            Destroy(gameObject);
            Instantiate(Coin, transform.position + new Vector3(0, 1, 0), transform.rotation);
            Instantiate(destroyedVersion, transform.position + new Vector3(0, -3, 0), transform.rotation);
        }

        inAttackRange = Vector3.Distance(transform.position, player.transform.position) < AttackRange;
        inLookAtRange = Vector3.Distance(transform.position, player.transform.position) < lookAtRange;

        if (inAttackRange) { transform.LookAt(player.transform); gameObject.GetComponent<Renderer>().material.color = Color.red; }
        else if (inLookAtRange) 
        { 
            transform.LookAt(player.transform); gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            Debug.DrawLine(player.transform.position, gameObject.transform.position, Color.yellow);
            gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
        }
        else gameObject.GetComponent<Renderer>().material.color = Color.green; ;

        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
           //Sound
            health -= 2;
            Destroy(collision.gameObject);
        }
    }
}
