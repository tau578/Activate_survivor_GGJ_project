using UnityEngine;

public class OxygonPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        OxygonSystem oxygonSystem = other.GetComponent<OxygonSystem>();
        if (oxygonSystem != null)
        {
            oxygonSystem.RefillOxygen();
            Destroy(gameObject);
        }
    }
}
