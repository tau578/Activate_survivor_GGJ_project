using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 20;
    [SerializeField] private string playerTag = "Player";

    [Header("Optional")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private bool destroyAfterPickup = true;

    private void Reset()
    {
        // Make the collider a trigger automatically when you add the script
        Collider c = GetComponent<Collider>();
        c.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        // PlayerShoot is on a child (PlayerShoot object), so use GetComponentInChildren
        PlayerShoot shooter = other.GetComponentInChildren<PlayerShoot>();
        if (shooter == null) return;

        shooter.AddAmmo(ammoAmount);

        // optional sound
        if (audioSource != null && pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }

        if (destroyAfterPickup)
        {
            Destroy(gameObject);
        }
        else
        {
            // If you don't destroy, at least disable collider so it can't be collected again
            Collider c = GetComponent<Collider>();
            if (c != null) c.enabled = false;
            // and optionally hide mesh
            var renderer = GetComponentInChildren<Renderer>();
            if (renderer != null) renderer.enabled = false;
        }
    }
}
