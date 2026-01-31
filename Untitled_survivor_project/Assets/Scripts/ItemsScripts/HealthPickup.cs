using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        OxygonSystem oxygonSystem = other.GetComponent<OxygonSystem>();
        if (oxygonSystem != null)
        {
            oxygonSystem.RefillHealth();
            Destroy(gameObject);
        }
    }
}
