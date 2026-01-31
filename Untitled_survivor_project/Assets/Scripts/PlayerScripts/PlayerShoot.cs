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

    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxShootDistance = 1000f;


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
        Vector3 targetPoint;

        // Ray from center of screen
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxShootDistance))
        {
            targetPoint = hit.point; // shoot where we hit
        }
        else
        {
            targetPoint = ray.origin + ray.direction * maxShootDistance;
        }

        // Direction from gun to target
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.MoveButllet(direction, bulletSpeed);
        }

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
