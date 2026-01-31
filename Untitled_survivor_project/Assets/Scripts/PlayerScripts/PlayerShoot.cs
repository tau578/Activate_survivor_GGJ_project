using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float fireRate = 0.5f; // bullets per second

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] gunShots;


    private float fireRaateTimer = 0f;
    private void Update()
    {
        fireRaateTimer += Time.deltaTime;
        if(Input.GetKey(KeyCode.Mouse0) && fireRaateTimer >= fireRate)
        {
            fireRaateTimer = 0f;
            Shoot();
        }
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.MoveButllet(firePoint.forward, bulletSpeed);
        }
       // Destroy(bullet, 2f); // Destroy the bullet after 2 seconds to avoid clutter

       PlayRandomShot();
    }

    private void PlayRandomShot()
    {
        if (gunShots == null || gunShots.Length == 0) return;

        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(
            gunShots[Random.Range(0, gunShots.Length)]
        );
    }



}
