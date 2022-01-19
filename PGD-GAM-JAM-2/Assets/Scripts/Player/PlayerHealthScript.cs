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
        HideDamageIndicator();
        currentHealth = maxHealth;
    }


    void Update()
    {
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
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void takeDamage(int damage)
    {
        if (isInvincible) return;


        currentHealth -= damage;

        ShowDamageIndicator();
        CancelInvoke("HideDamageIndicator");
        Invoke("HideDamageIndicator", damageDuration);
        StartCoroutine(BecomeInvincible());
    }
    public void ShowDamageIndicator() { damageIndicator.SetActive(true); }
    public void HideDamageIndicator() { damageIndicator.SetActive(false); }
    public void FadeOut()
    {
        HudDeath.SetActive(true);
        StartCoroutine(FadeOutEmum());
        
        
    }
    public void FadeIn()
    {
        StartCoroutine(FadeInEmum());
    }
    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Health/Damage");
        yield return new WaitForSeconds(invincibilityDurationSeconds);


        isInvincible = false;
    }
    private IEnumerator FadeInEmum()
    {
        HudDeath.GetComponent<Animation>().Play("FadeInAnim");

        yield return new WaitForSeconds(3f);


        HudDeath.SetActive(false);
    }
    private IEnumerator FadeOutEmum()
    {
        HudDeath.GetComponent<Animation>().Play("DeathFade");

        yield return new WaitForSeconds(1.4f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
