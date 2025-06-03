using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed = 20f;

    [SerializeField] private Transform firePoint;

    [Header("Turret Stats")]
    [SerializeField][Tooltip("in Sekunden")] private float shootInterval = 2f;
    [SerializeField] private float checkRadius = 5f;
    [SerializeField] private string enemyTag = "Enemy";

    private float shootTimer;

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            GameObject target = FindNearestEnemy();
            if (target != null)
            {
                Shoot(target.transform);
                shootTimer = 0f;
            }
        }
    }

    private GameObject FindNearestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, checkRadius);
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag(enemyTag))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = hit.gameObject;
                }
            }
        }

        return nearestEnemy;
    }

    private void Shoot(Transform target)
    {
        if (bullet != null && firePoint != null)
        {
            GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
            newBullet.transform.SetParent(firePoint);

            Vector3 direction = (target.position - firePoint.position).normalized;

            Rigidbody rb = newBullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(direction * bulletSpeed, ForceMode.Impulse);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
