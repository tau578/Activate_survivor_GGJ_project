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
    private void Start()
    {
        Destroy(gameObject, 4f); // Destroy bullet after 5 seconds to avoid clutter
    }
    public void MoveButllet(Vector3 direction, float speed)
    {
        rb.AddForce(direction.normalized * speed, ForceMode.VelocityChange);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTrigger Enter got callled");
        Health healthComponent = other.GetComponent<Health>();
        if (healthComponent != null)
        {
            Debug.Log("trying to deal damage to " + other.name);
            healthComponent.DealDamage(damage);
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollision Enter got callled");
        Health healthComponent = collision.gameObject.GetComponent<Health>();
        if (healthComponent != null)
        {
            Debug.Log("trying to deal damage to " + collision.gameObject.name);
            healthComponent.DealDamage(damage);
        }
        Destroy(gameObject);
    }
}
