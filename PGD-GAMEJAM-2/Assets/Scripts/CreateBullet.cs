using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBullet : MonoBehaviour
{

    public Rigidbody bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody projectile;

        if (Input.GetMouseButtonDown(0)) 
        {
            projectile = Instantiate(bullet, new Vector3(
                transform.position.x + 1.5f,
                transform.position.y + 0.75f,
                transform.position.z),
                Quaternion.identity);

            projectile.transform.position += projectile.velocity;
            projectile.AddForce(transform.right * 50, ForceMode.Impulse);
        }        
    }
}
