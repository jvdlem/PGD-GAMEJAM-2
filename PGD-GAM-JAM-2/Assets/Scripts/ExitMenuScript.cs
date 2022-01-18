using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenuScript : MonoBehaviour
{
    public bool menuOn;
    public FMOD.Studio.Bus MasterBus;
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(false);
        MasterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main");
    }

    public void Exit()
    {
        //this should work in a build... 
        Application.Quit();
    }

    public void ToggleExitMenuScreenOn()
    {
        menuOn = true;
        gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ToggleExitMenuScreenOff()
    {
        menuOn = false;
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
