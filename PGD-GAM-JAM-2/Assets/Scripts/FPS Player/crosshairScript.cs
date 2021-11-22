using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshairScript : MonoBehaviour
{
    [SerializeField] playerAimScript playeraiming;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (playeraiming.aiming)
        {
            gameObject.SetActive(false);
        }   
    }

    public void turnBackOn()
    {
        gameObject.SetActive(true);
    }
}
