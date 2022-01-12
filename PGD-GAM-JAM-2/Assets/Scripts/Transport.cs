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

    public void StartTransport()
    {
        StartCoroutine(TransportPlayer());
    }
    public IEnumerator TransportPlayer()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("cave system");
       
        /**
        doitpls = true;
        if (goal == null)
        {
            this.transform.position = worldPos;
            //player.GetComponent<PlayerHealthScript>().FadeIn();
            doitpls = true;
        }
        else
        {
            yield return new WaitForSeconds(5f);

            this.transform.position = goal.transform.position;
            player.GetComponent<PlayerHealthScript>().FadeIn();
        }
        **/
    }
}
