using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] deathScreenScript deathscreenscript;

    [Header("Hud")]
    public GameObject damageIndicator;
    public GameObject HudDeath;

    [Header("Health")]
    public int maxHealth;
    public int currentHealth;

    [Header("Damage")]
    public float damageDuration = 0.5f;
    [SerializeField] public float invincibilityDurationSeconds;
    public bool isInvincible = false;

    [Header("Resources")]
    public int coins = 0;



    void Start()
    {
        //make sure damageindicator is hidden
        //set current health to maxhealth
        HideDamageIndicator();
        currentHealth = maxHealth;
    }


    void Update()
    {
        //always check if the players health is either exciding the limit or the player is at 0 health
        MaxHealth();
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            if (HudDeath != null)
            {
                FadeOut();
            }

            if (deathscreenscript != null)
            {
                deathscreenscript.ToggleDeathScreen();
            }
        }
    }

    void MaxHealth()
    {
        //health cant go above max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    //other scripts will call this to give damage
    public void takeDamage(int damage)
    {
        if (isInvincible) return;


        currentHealth -= damage;
        //simple way to show the player has taken damage
        ShowDamageIndicator();
        CancelInvoke("HideDamageIndicator");
        Invoke("HideDamageIndicator", damageDuration);
        //make sure player goes invincible for a set duration
        StartCoroutine(BecomeInvincible());
    }
    
    public void ShowDamageIndicator() { damageIndicator.SetActive(true); }
    public void HideDamageIndicator() { damageIndicator.SetActive(false); }
    public void FadeOut()
    {
        //used when death happens or player begins teleport...
        HudDeath.SetActive(true);
        StartCoroutine(FadeOutEmum());
        
        
    }
    public void FadeIn()
    {
        //used when fading in from a teleport...
        StartCoroutine(FadeInEmum());
    }
    private IEnumerator BecomeInvincible()
    {
        //give the player I frames after taking damage so enemies cant instakill
        isInvincible = true;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Health/Damage");
        yield return new WaitForSeconds(invincibilityDurationSeconds);


        isInvincible = false;
    }
    private IEnumerator FadeInEmum()
    {
        // for when the player fades back in after teleport?
        HudDeath.GetComponent<Animation>().Play("FadeInAnim");

        yield return new WaitForSeconds(3f);


        HudDeath.SetActive(false);
    }
    private IEnumerator FadeOutEmum()
    {
        // for when the player dies
        HudDeath.GetComponent<Animation>().Play("DeathFade");

        yield return new WaitForSeconds(1.4f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
