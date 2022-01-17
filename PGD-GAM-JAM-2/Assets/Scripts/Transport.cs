using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transport : MonoBehaviour
{
    public GameObject goal;
    public GameObject player;
    public Vector3 worldPos;
    public bool doitpls;

    public void StartTransportToGame()
    {
        StartCoroutine(TransportToGame());
    }
    public IEnumerator TransportToGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("cave system");
    }

    public void StartTransportToMenu()
    {
        StartCoroutine(TransportPlayerToMenu());
    }
    public IEnumerator TransportPlayerToMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Main");
    }
}


