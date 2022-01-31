using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(countdown());
    }

    IEnumerator countdown()
    {
        yield return new WaitForSeconds(5);
        this.gameObject.SetActive(false);
    }
}
