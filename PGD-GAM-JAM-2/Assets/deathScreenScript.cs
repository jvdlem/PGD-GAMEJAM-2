using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class deathScreenScript : MonoBehaviour
{

    public GameObject gun;
    public PlayerHealthScript healthScript;
    private bool isActive = false;
    public int amountOfDeaths;
    private void Start()
    {
        gameObject.SetActive(false);
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
        Analytics.CustomEvent("Amount of Deaths" + amountOfDeaths);
        Application.Quit();
    }

    public void ToggleDeathScreen()
    {
        gameObject.SetActive(true);
        healthScript.isInvincible = true;
        healthScript.invincibilityDurationSeconds = 10000;
        gun.GetComponent<Pistol>().isInMenu = isActive;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        amountOfDeaths++;
    }
}
