using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;
    }

    public void MoveButllet(Vector3 direction, float speed)
    {
        rb.AddForce(direction.normalized * speed, ForceMode.VelocityChange);
    }
    private void OnTriggerEnter(Collider other)
    {
        /*
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
        Destroy(gameObject);
        */
    }
}
