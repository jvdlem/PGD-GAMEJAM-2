using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WatchUi : MonoBehaviour
{
    //Angles that the players watch will open if looked at 
    [SerializeField] float zAngleMin;
    [SerializeField] float zAngleMax;

    public Animation WatchUiAnim;
    public Animation WatchFaceAnim;
    public Text healthText;
    public Text coinCount;
    public bool lookedAt;
    private PlayerHealthScript PlayerScript;
    private int state;

    public Canvas WatchCanvas;

    private bool WatchUiOpen;
    private void Start()
    {
        WatchUiOpen = false;
     
      




        PlayerScript = FindObjectOfType<PlayerHealthScript>();
        WatchCanvas = WatchCanvas.GetComponent<Canvas>();

    }

    private void Update()
    {
        //Set the current health and coins of the player on the UI.
        healthText.text = "" + PlayerScript.currentHealth;
        coinCount.text = "" + PlayerScript.coins;
        //if (this.transform.eulerAngles.z >= zAngleMin && this.transform.eulerAngles.z <= zAngleMax && !WatchUiOpen && lookedAt)
        //{

        //    FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Watch/Open Watch");
        //    WatchFaceAnim.Play("WatchFaceAnimation");
        //    WatchUiAnim.Play("WatchAnimOpen");
        //    WatchUiOpen = true;
        //}
        //else if (this.transform.eulerAngles.z <= zAngleMin && WatchUiOpen || this.transform.eulerAngles.z >= zAngleMax && WatchUiOpen)
        //{
        //    FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Watch/Close Watch");
        //    WatchFaceAnim.Play("WatchFaceAnimationClose");
        //    WatchUiAnim.Play("WatchAnimClose");
        //    WatchUiOpen = false;
        //}

        if (lookedAt && state != 2)
        {
            // if looked at and state is not already 2 open the watch
            state = 1;
        }
        else if ( this.transform.eulerAngles.z <= zAngleMin && WatchUiOpen && state != 0 || this.transform.eulerAngles.z >= zAngleMax && WatchUiOpen && state != 0)
        {
            //Watch is not within the angles acceptable for it to open go to state 3 where the watch closes if we are not already closed.
            state = 3;
        }
        switch (state)
        {
            case 0:
                WatchCanvas.enabled = false;
                break;
            case 1:
                WatchCanvas.enabled = true;
                state = 2;
                break;
            case 2:
                if (this.transform.eulerAngles.z >= zAngleMin && this.transform.eulerAngles.z <= zAngleMax && !WatchUiOpen )
                {

                    FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Watch/Open Watch");
                    WatchFaceAnim.Play("WatchFaceAnimation");
                    WatchUiAnim.Play("WatchAnimOpen");
                    WatchUiOpen = true;
                }
                break;
            case 3:
                  
               
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Watch/Close Watch");
                    WatchFaceAnim.Play("WatchFaceAnimationClose");
                    WatchUiAnim.Play("WatchAnimClose");
                    WatchUiOpen = false;
                    state = 0;
                
                break;
           

        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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


}
