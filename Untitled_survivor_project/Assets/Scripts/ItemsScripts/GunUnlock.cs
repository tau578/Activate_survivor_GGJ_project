using UnityEngine;

public class GunUnlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerShoot playerShoot = other.GetComponentInChildren<PlayerShoot>();
        if (playerShoot != null)
        {
            playerShoot.UnlockGun();
            Destroy(gameObject);
        }
    }
}
