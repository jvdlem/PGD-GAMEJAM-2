using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WarningScreenScript : MonoBehaviour
{
    private float WaitForFadeTime = 4;
    private float WaitForFadeTimer;

    private float WaitTime = 6;
    private float WaitTimer;

    public TextMeshProUGUI[] Texts;

    private float timeElapsed;
    private float LerpDuration = 4;

    private void Start()
    {
        WaitTimer = 0;
    }

    void Update()
    {
        if (WaitForFadeTimer < WaitForFadeTime)
        {
            WaitForFadeTimer += Time.deltaTime;
        }
        else
        {
            foreach (TextMeshProUGUI Text in Texts)
            {
                Text.alpha = Mathf.MoveTowards(1, 0, timeElapsed / LerpDuration);
                timeElapsed += Time.deltaTime;
            }
        }

        if (WaitTimer < WaitTime)
        {
            WaitTimer += Time.deltaTime;
        }
        else
        {
            WaitTimer = 0;
            WaitForFadeTimer = 0;
            SceneManager.LoadScene("Main");
        }
    }
}
