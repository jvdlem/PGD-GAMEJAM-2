using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    [Header("Values")]
    public float RotationSpeed;
    public float lerpSpeed = 3;
    private float t;

    private void Start()
    {
        RotationSpeed = Random.Range(60, 120);
    }
    void Update()
    {
        t += Time.deltaTime;
        transform.position += -transform.up * Time.deltaTime * Mathf.Sin(t * Mathf.PI ) * lerpSpeed / Mathf.PI * 2f;
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            other.GetComponent<PlayerHealthScript>().coins++;
            Destroy(gameObject);
        }
    }
}
