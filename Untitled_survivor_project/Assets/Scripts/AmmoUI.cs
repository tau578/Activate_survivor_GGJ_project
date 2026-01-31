using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private TMP_Text ammoText;

    private void Awake()
    {
        // Optional auto-find if you forgot to assign
        if (playerShoot == null)
            playerShoot = FindFirstObjectByType<PlayerShoot>();

        if (ammoText == null)
            ammoText = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        if (playerShoot == null || ammoText == null) return;

        ammoText.text = $"{playerShoot.CurrentAmmo} / {playerShoot.MaxAmmo}";
    }
}
