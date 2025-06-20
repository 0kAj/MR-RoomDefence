using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 20f;
    public float rotateSpeed = 200f;
    public float explosionRadius = 5f;
    public float damage = 50f;
    public GameObject explosionEffect;

    private Transform target;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        // Initialer Impuls nach oben
        rb.linearVelocity = Vector3.up * 10f;
        Invoke(nameof(ActivateHoming), 0.5f); // nach kurzer Zeit Zielverfolgung aktivieren
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void ActivateHoming()
    {
        StartCoroutine(Homing());
    }

    System.Collections.IEnumerator Homing()
    {
        while (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            // Rotation zur Zielrichtung
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            // Bewegung nach vorne
            rb.linearVelocity = transform.forward * speed;

            yield return null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(700f, transform.position, explosionRadius);

            //Health hp = nearby.GetComponent<Health>();
            //if (hp != null)
            //    hp.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
