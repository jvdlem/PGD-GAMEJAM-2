using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WatchUi : MonoBehaviour
{
    public GameObject Canvas;
    public Animation WatchUiAnim;
    public Text healthText;
    public Text coinCount;
    private PlayerHealthScript PlayerScript;
    private bool WatchUiOpen;
    private void Start()
    {
        PlayerScript = FindObjectOfType<PlayerHealthScript>();
    }

    private void Update()
    {
        
        healthText.text = "" + PlayerScript.currentHealth;
        coinCount.text = "" + PlayerScript.coins;
        if (this.transform.eulerAngles.z >= 150f && this.transform.eulerAngles.z <= 260f && !WatchUiOpen)
        {

            Canvas.SetActive(true);
            WatchUiAnim.Play("WatchAnimOpen");
            WatchUiOpen = true;

        }
        else if (this.transform.eulerAngles.z <= 150f && WatchUiOpen || this.transform.eulerAngles.z >= 260f && WatchUiOpen)
        {
            WatchUiAnim.Play("WatchAnimClose");
            
            
            WatchUiOpen = false;
            
        }
        
    }


}
