using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float lifeTime = 4f;

    private Vector3 dir;
    private float speed;

    // Small radius to make hit detection reliable
    [SerializeField] private float hitRadius = 0.05f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void MoveButllet(Vector3 direction, float bulletSpeed)
    {
        dir = direction.normalized;
        speed = bulletSpeed;
    }

    private void Update()
    {
        float distanceThisFrame = speed * Time.deltaTime;

        // SphereCast to detect hit even at high speed (no tunneling)
        if (Physics.SphereCast(transform.position, hitRadius, dir, out RaycastHit hit, distanceThisFrame))
        {
            Health healthComponent = hit.collider.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.DealDamage(damage);
            }

            Destroy(gameObject);
            return;
        }

        // Move straight
        transform.position += dir * distanceThisFrame;
    }
}
