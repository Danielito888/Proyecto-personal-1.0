using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    public float maxSanity = 100f;
    public float maxHunger = 100f;
    public float maxThirst = 100f;

    public float health;
    public float sanity;
    public float hunger;
    public float thirst;

    [Header("Decay Rates")]
    public float sanityDecayRate = 1f;     // Por minuto
    public float hungerDecayRate = 3f;     // Por minuto
    public float thirstDecayRate = 5f;     // Por minuto

    [Header("UI Images")]
    public Image healthCircle;
    public Image sanityCircle;
    public Image hungerCircle;
    public Image thirstCircle;

    void Start()
    {
        health = maxHealth;
        sanity = maxSanity;
        hunger = maxHunger;
        thirst = maxThirst;

        UpdateUI();
    }

    void Update()
    {
        float delta = Time.deltaTime;

        sanity -= sanityDecayRate * (delta / 60f);
        hunger -= hungerDecayRate * (delta / 60f);
        thirst -= thirstDecayRate * (delta / 60f);

        // Si hambre llega a 0, baja salud
        if (hunger <= 0)
        {
            health -= 5f * delta;
        }

        if (thirst <= 0)
        {
            health -= 5f * delta;
        }

        ClampStats();
        UpdateUI();
    }

    void ClampStats()
    {
        health = Mathf.Clamp(health, 0f, maxHealth);
        sanity = Mathf.Clamp(sanity, 0f, maxSanity);
        hunger = Mathf.Clamp(hunger, 0f, maxHunger);
        thirst = Mathf.Clamp(thirst, 0f, maxThirst);
    }

    void UpdateUI()
    {
        if (healthCircle) healthCircle.fillAmount = health / maxHealth;
        if (sanityCircle) sanityCircle.fillAmount = sanity / maxSanity;
        if (hungerCircle) hungerCircle.fillAmount = hunger / maxHunger;
        if (thirstCircle) thirstCircle.fillAmount = thirst / maxThirst;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        ClampStats();
    }

    public void RestoreSanity(float amount)
    {
        sanity += amount;
        ClampStats();
    }

    public void EatFood(float amount)
    {
        hunger += amount;
        ClampStats();
    }

    public void DrinkWater(float amount)
    {
        thirst += amount;
        ClampStats();
    }
}
