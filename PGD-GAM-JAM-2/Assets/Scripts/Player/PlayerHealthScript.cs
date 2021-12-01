using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [Header("Hud")]
    public GameObject damageIndicator;
    public GameObject HudDeath;

    [Header("Health")]
    public int maxHealth;
    public int currentHealth;

    [Header("Damage")]
    public float damageDuration = 0.5f;
    [SerializeField] private float invincibilityDurationSeconds;
    private bool isInvincible = false;
    


    void Start()
    {
        HideDamageIndicator();
        currentHealth = maxHealth;
    }


    void Update()
    {
        MaxHealth();
     
    }

    void MaxHealth() 
    {
        if (currentHealth > maxHealth) {
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


        if (currentHealth <= 0) 
        {
            currentHealth = 0;
            FadeOut();
            //die
        }

        StartCoroutine(BecomeInvincible());
    }
    public void ShowDamageIndicator() { damageIndicator.SetActive(true); }
    public void HideDamageIndicator() { damageIndicator.SetActive(false); }
    public void FadeOut() 
    {
        HudDeath.SetActive(true);
        HudDeath.GetComponent<Animation>().Play("DeathFade");
    }
    public void FadeIn() 
    {
        StartCoroutine(FadeInEmum());
    }
    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDurationSeconds);


        isInvincible = false;
    }
    private IEnumerator FadeInEmum() 
    {
        HudDeath.GetComponent<Animation>().Play("FadeInAnim");

        yield return new WaitForSeconds(3f);


        HudDeath.SetActive(false);
    }
}
