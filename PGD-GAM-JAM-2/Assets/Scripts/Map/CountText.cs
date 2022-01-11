using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountText: MonoBehaviour
{
    [SerializeField] private int amountOfThings = 0;
    private int buttonCount = 0;
    private Text myText;
    [SerializeField] private string fisrtText = "";

    private void Start()
    {
        myText = GetComponent<Text>();
        myText.text = fisrtText + buttonCount + " / " + amountOfThings;
    }
    public void addCount(int add)
    {
        buttonCount += add;
        myText.text = fisrtText + buttonCount + " / " + amountOfThings;
    }
}
