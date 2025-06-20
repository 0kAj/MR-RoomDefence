using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public GameObject rocketPrefab;
    public Transform launchPoint;
    public float fireRate = 3f;
    private float fireCooldown;
    [SerializeField] private float checkRadius = 5f;
    [SerializeField] private string enemyTag = "Enemy";

    void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            GameObject target = FindNearestEnemy();
            if (target != null)
            {
                LaunchRocket(target.transform);
                fireCooldown = fireRate;
            }
        }
    }

    void LaunchRocket(Transform target)
    {
        GameObject rocket = Instantiate(rocketPrefab, launchPoint.position, Quaternion.identity);
        Rocket rocketScript = rocket.GetComponent<Rocket>();
        if (rocketScript != null)
        {
            rocketScript.SetTarget(target);
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



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
