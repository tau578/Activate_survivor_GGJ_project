using UnityEngine;

public class OxygonSystem : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Health playerHealth;
    [SerializeField] private UIManager uiManager;
    [Header("Oxygen Settings")]
    [SerializeField] private float maxOxygen = 100f;
    [SerializeField] private float oxygenDepletionRate = 1f; // per second
    [SerializeField] private float oxygenDepletionRateWalking = 2f; // per second
    [SerializeField] private float oxygenDepletionRateRunning = 5f; // per second
    [Header("Damage Settings")]
    [SerializeField] private float takeDamageInterval = 2f; // takes damage every 2 seconds when out of oxygen
    [SerializeField] private float damagePerInterval = 3;
    private float damageTimer;
    private float currentOxygen;
    private void Awake()
    {
        if (playerController == null)
        {
            playerController = FindAnyObjectByType<PlayerController>();
        }
        if (uiManager == null)
        {
            uiManager = FindAnyObjectByType<UIManager>();
        }
    }
    private void Start()
    {
        currentOxygen = maxOxygen;
    }
    private void Update()
    {
        TakeDamageDueToOxygenDepletion();
        if (playerController.isRunning)
        {
            currentOxygen -= oxygenDepletionRateRunning * Time.deltaTime;
        }
        else if (playerController.isWalking)
        {
            currentOxygen -= oxygenDepletionRateWalking * Time.deltaTime;
        }
        else
        {
            currentOxygen -= oxygenDepletionRate * Time.deltaTime;
        }
        UpdaatePlayerUI();
    }
    private void TakeDamageDueToOxygenDepletion()
    {
        if (currentOxygen <= 0)
        {
            damageTimer += Time.deltaTime;
            if(damageTimer >= takeDamageInterval)
            {
                damageTimer = 0f;
                playerHealth.DealDamage(damagePerInterval); // Damage over time when out of oxygen
            } 
        }
    }
    private void UpdaatePlayerUI()
    {
        uiManager.UpdateOxygenBar(currentOxygen, maxOxygen);
        uiManager.UpdateHealthBar(playerHealth.health, playerHealth.maxHealth);
    }
    public void RefillOxygen()
    {
        currentOxygen = maxOxygen;
    }
    public void RefillHealth()
    {
        playerHealth.RefilHealth();
    }

}
