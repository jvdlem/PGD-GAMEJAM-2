using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AttachmentStatsUi : MonoBehaviour
{
    [SerializeField] public Canvas attachmentStatsUi;
    public int currentState;
    public List<float> statList;
    public  Text[] TstatList;
    [Header("Stats")]
    [SerializeField] float spread;
    [SerializeField] float amountOfBullets;
    [SerializeField] float damage;
    [SerializeField] float range;





    public void Update()
    {
        attachmentStatsUi = GetComponentInChildren<Canvas>();
        TstatList = GetComponentsInChildren<Text>();
        state();
        getStats();
        showStats();
        
     

        currentState = default;
    }
    public void state()
    {
        switch (currentState)
        {
            case 1:
                attachmentStatsUi.GetComponent<Canvas>().enabled = true;
                break;
            case 2:

            default:
                attachmentStatsUi.GetComponent<Canvas>().enabled = false;
                break;
        }
    }
    public void getStats()
    {

        statList = GetComponent<AttachmentStats>().statList;
        spread          = statList[0];
        amountOfBullets = statList[1];
        damage          = statList[2];
        range           = Mathf.Abs(statList[3] * statList[4]);

    }
    public void showStats()
    { 
        //Tspread.text = "Spread = " + spread;
        //TamountOfBullets.text = "Bullets = " + amountOfBullets;
        //Tdamage.text = "Damage = " + damage;
        //Trange.text = "Range = " + range;
        TstatList[0].GetComponent<Text>().text = "Spread = " + spread;
        TstatList[1].GetComponent<Text>().text = "Bullets = " + amountOfBullets;
        TstatList[2].GetComponent<Text>().text = "Damage = " + damage;
        TstatList[3].GetComponent<Text>().text = "Range = " + range;
    }
}
