using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WatchUi : MonoBehaviour
{
    public GameObject Canvas;
    public Animation WatchUiAnim;
    public Text healthText;
    private void Update()
    {
        healthText.text = "" + FindObjectOfType<PlayerHealthScript>().currentHealth;
        if (this.transform.eulerAngles.z >= 90f && this.transform.eulerAngles.z >= 200f)
        {
            Canvas.SetActive(true);
            WatchUiAnim.Play("WatchAnimOpen");
            
        }
        else
        { 
            Canvas.SetActive(false);
            WatchUiAnim.Play("WatchAnimClose");
        }
        
    }


}
