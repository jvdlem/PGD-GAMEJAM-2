using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transport : MonoBehaviour
{
    private FMOD.Studio.Bus MasterBus;

    private void Start()
    {
        MasterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
    }

    public void StartTransportToGame()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(TransportToGame());
        }
    }
    public IEnumerator TransportToGame()
    {
        yield return new WaitForSeconds(5f);
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("cave system");
    }

    public void StartTransportToMenu()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(TransportPlayerToMenu());
        }
    }
    public IEnumerator TransportPlayerToMenu()
    {
        yield return new WaitForSeconds(5f);
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("Main");
    }
}


