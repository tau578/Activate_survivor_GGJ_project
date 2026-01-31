using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image oxygenBar;
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider playerOxygenBar;
    public void UpdateOxygenBar(float currentOxygen, float maxOxygen)
    {
        if (oxygenBar != null)
        {
            playerOxygenBar.value = currentOxygen / maxOxygen;
        }
    }
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (playerHealthBar != null)
        {
            playerHealthBar.value = currentHealth / maxHealth;
        }
    }
}
