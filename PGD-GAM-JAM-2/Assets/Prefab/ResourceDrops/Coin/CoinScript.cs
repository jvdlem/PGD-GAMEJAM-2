using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Collider coinCollider;
    [Header("Values")]
    public float distanceFromGround;
    public float rotationSpeed;
    public float lerpSpeed = 15;
    public float maxDistanceFromPlayer = 30;
    private float t , extents;
    LayerMask ground;
    public GameObject Player;

    private void Start()
    {
        //Make a ground layerMask
        ground = LayerMask.GetMask("Ground");
        //Set rotation speed of the GameObject
        rotationSpeed = Random.Range(60, 120);
        //Find the GameObject Player
        Player = GameObject.FindGameObjectWithTag("Player");
        //Find the collider for this GameObject
        coinCollider = GetComponent<Collider>();
        extents = coinCollider.bounds.extents.y;
        
    }
    void Update()
    {
        //Create a up and down movement with a Sin wave
        t += Time.deltaTime;
        transform.position += -transform.up * Time.deltaTime * Mathf.Sin(t * Mathf.PI ) * lerpSpeed / Mathf.PI * 2f;
        if (Vector3.Distance(Player.transform.position, this.transform.position) < maxDistanceFromPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 3 * Time.deltaTime);
        }
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);


        //Send a ray down checking for the ground

        Ray ray = new Ray(transform.position + new Vector3(0, extents, 0), transform.TransformDirection(Vector3.down));
        RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0, extents , 0), transform.TransformDirection(Vector3.down) * distanceFromGround);
        if (Physics.Raycast(ray, out hit, distanceFromGround, ground))
        {

            transform.position = Vector3.MoveTowards(transform.position, transform.position += Vector3.up * extents/2, 1 * Time.deltaTime);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/MISC/CoinPickup");
            other.GetComponent<PlayerHealthScript>().coins++;
            Destroy(gameObject);
        }
    }
}
