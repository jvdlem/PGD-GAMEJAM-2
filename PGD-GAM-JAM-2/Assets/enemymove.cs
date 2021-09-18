using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour
{
    [SerializeField] GameObject player;
    float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}
