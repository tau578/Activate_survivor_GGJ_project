using UnityEngine;

public class OxygonSystem : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float maxOxygen = 100f;
    [SerializeField] private float oxygenDepletionRate = 1f; // per second
    [SerializeField] private float oxygenDepletionRateWalking = 2f; // per second
    [SerializeField] private float oxygenDepletionRateRunning = 5f; // per second
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
        uiManager.UpdateOxygenBar(currentOxygen, maxOxygen);
    }
    public void RefillOxygen()
    {
        currentOxygen = maxOxygen;
    }

}
