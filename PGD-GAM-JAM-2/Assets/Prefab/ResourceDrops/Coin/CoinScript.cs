using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    [Header("Values")]
    public float RotationSpeed;
    public float lerpSpeed = 3;
    private float t;

    public GameObject Player;

    private void Start()
    {
        RotationSpeed = Random.Range(60, 120);
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        t += Time.deltaTime;
        transform.position += -transform.up * Time.deltaTime * Mathf.Sin(t * Mathf.PI ) * lerpSpeed / Mathf.PI * 2f;
        if (Vector3.Distance(Player.transform.position, this.transform.position) < 10)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 3 * Time.deltaTime);
        }
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            other.GetComponent<PlayerHealthScript>().coins++;
            Destroy(gameObject);
        }
    }
}
