using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WatchUi : MonoBehaviour
{
    public GameObject Canvas;
    public Animation WatchUiAnim;
    public Text healthText;
    private bool WatchUiOpen;
    private void Update()
    {
        healthText.text = "" + FindObjectOfType<PlayerHealthScript>().currentHealth;
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
