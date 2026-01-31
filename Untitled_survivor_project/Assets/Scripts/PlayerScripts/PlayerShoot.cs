using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Gun")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;

    [Tooltip("Time between shots in seconds (0.5 = 2 shots/second)")]
    [SerializeField] private float timeBetweenShots = 0.5f;

    [Header("Ammo")]
    [SerializeField] private int maxAmmo = 20;
    [SerializeField] private int currentAmmo = 0; // start empty (set to maxAmmo if you want full at start)

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] gunShots;

    [Header("Aim")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxShootDistance = 1000f;

    private float shotTimer = 0f;

    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => maxAmmo;

    private void Update()
    {
        shotTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && shotTimer >= timeBetweenShots)
        {
            if (currentAmmo <= 0) return; // no ammo => can't shoot

            shotTimer = 0f;
            currentAmmo--; // spend 1 bullet
            Shoot();
        }
    }

    private void Shoot()
    {
        /*
        if (playerCamera == null || firePoint == null || bulletPrefab == null) return;

        // Ray from center of screen
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;
        bool hitSomething = Physics.Raycast(ray, out RaycastHit hit, maxShootDistance);

        if (hitSomething)
            targetPoint = hit.point;
        else
            targetPoint = ray.origin + ray.direction * maxShootDistance;

        // Direction from gun to target
        Vector3 direction = (targetPoint - firePoint.position).normalized;
        

        // ---- SAFE SPAWN (the fix) ----
        // push the bullet a bit forward from muzzle so it doesn't overlap colliders
        const float muzzleOffset = 0.15f;   // tweak: 0.1 - 0.3 depending on your gun
        const float backOffFromHit = 0.05f; // keep bullet slightly before the surface when extremely close

        Vector3 spawnPos = firePoint.position + direction * muzzleOffset;

        // If target is extremely close, spawn just before the hit point instead
        if (hitSomething)
        {
            float distToHit = Vector3.Distance(firePoint.position, hit.point);

            // if hit is closer than where we'd spawn, clamp spawn to just before the surface
            if (distToHit <= muzzleOffset + backOffFromHit)
            {
                spawnPos = hit.point - direction * backOffFromHit;
            }
        }
        */
      //  GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.LookRotation(direction));
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Vector3 direction = firePoint.forward;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.MoveButllet(direction, bulletSpeed);
        }

        PlayRandomShot();
    }

    public void AddAmmo(int amount)
    {
        if (amount <= 0) return;
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo);
    }

    // Optional demonstrated helper if you want to set ammo directly
    public void SetAmmo(int amount)
    {
        currentAmmo = Mathf.Clamp(amount, 0, maxAmmo);
    }

    private void PlayRandomShot()
    {
        if (audioSource == null) return;
        if (gunShots == null || gunShots.Length == 0) return;

        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(gunShots[Random.Range(0, gunShots.Length)]);
    }
}
