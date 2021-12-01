using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FPSUI : MonoBehaviour
{
    [SerializeField] public Text healthText;
   
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "" + FindObjectOfType<PlayerHealthScript>().currentHealth;
    }

    public void ToggleFPSUI()
    {
        gameObject.SetActive(true);
    }
}
