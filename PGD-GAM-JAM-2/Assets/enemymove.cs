using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour
{
    [SerializeField] GameObject player;
    float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Sphere");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < 10)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
