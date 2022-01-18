using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WatchUi : MonoBehaviour
{
    [SerializeField] float zAngleMin;
    [SerializeField] float zAngleMax;
  
    public Animation WatchUiAnim;
    public Animation WatchFaceAnim;
    public Text healthText;
    public Text coinCount;
    public bool lookedAt;
    private PlayerHealthScript PlayerScript;
    private bool WatchUiOpen;
    private void Start()
    {
        WatchUiOpen = false;
        zAngleMax = 210;
        zAngleMin = 150;

        
          
        PlayerScript = FindObjectOfType<PlayerHealthScript>();
    }

    private void Update()
    {
        
        healthText.text = "" + PlayerScript.currentHealth;
        coinCount.text = "" + PlayerScript.coins;
        if (this.transform.eulerAngles.z >= zAngleMin && this.transform.eulerAngles.z <= zAngleMax && !WatchUiOpen && lookedAt)
        {
            
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Watch/Open Watch");
            WatchFaceAnim.Play("WatchFaceAnimation");
            WatchUiAnim.Play("WatchAnimOpen");
            WatchUiOpen = true;
        }
        else if (this.transform.eulerAngles.z <= zAngleMin && WatchUiOpen || this.transform.eulerAngles.z >= zAngleMax && WatchUiOpen )
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Watch/Close Watch");
            WatchFaceAnim.Play("WatchFaceAnimationClose");
            WatchUiAnim.Play("WatchAnimClose");
            WatchUiOpen = false;
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
