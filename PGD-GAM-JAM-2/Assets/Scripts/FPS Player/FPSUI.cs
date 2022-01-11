using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FPSUI : MonoBehaviour
{
    [SerializeField] public Text healthText;
    [SerializeField] public Text coinText;
    [SerializeField] public Text pressButtonText;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "" + FindObjectOfType<PlayerHealthScript>().currentHealth;
        coinText.text = "" + FindObjectOfType<PlayerHealthScript>().coins;
        if (FindObjectOfType<PushButtons>().buttonCanBePressed == true) pressButtonText.text = "Press E";
        else pressButtonText.text = "";
    }

    public void ToggleFPSUI()
    {
        gameObject.SetActive(true);
    }
}
