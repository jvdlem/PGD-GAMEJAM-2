using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bullet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Shoot();
    }

    void Shoot()
    {
        //Create a new instantiation of an arrow
        Rigidbody currentArrow = Instantiate(bullet, this.transform.position + new Vector3(0,1,1), Quaternion.identity).GetComponent<Rigidbody>();

        currentArrow.AddForce(transform.forward * 10f, ForceMode.Impulse);
        currentArrow.AddForce(transform.up * .25f, ForceMode.Impulse);
    }
}
