using UnityEngine;
using Oculus.Interaction;

public class SpawnBall : MonoBehaviour
{
    [Header("Projectile Spawn")]
    public GameObject prefab;
    public float spawnSpeed = 10;

    [Header("Ray Hit Spawn")]
    public GameObject spawnAtRayHitPrefab;
    public RayInteractor rightRayInteractor;
    public float raycastDistance = 10f;

    void Update()
    {
        // Shoot ball from controller
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            GameObject spawnedBall = Instantiate(prefab, transform.position, Quaternion.identity);
            Rigidbody spawnedBallRB = spawnedBall.GetComponent<Rigidbody>();
            spawnedBallRB.linearVelocity = transform.forward * spawnSpeed;
        }

        // Spawn prefab at raycast hit location (nicht unbedingt beim OculusCursor)
        if (OVRInput.GetDown(OVRInput.RawButton.B) && rightRayInteractor != null)
        {
            Ray ray = rightRayInteractor.Ray;
            LayerMask layerMask = LayerMask.GetMask("Default", "Environment");
            if (Physics.Raycast(ray, out RaycastHit hitInfo, raycastDistance, layerMask))
            {
                Vector3 hitPoint = hitInfo.point;
                //  Quaternion rotation = Quaternion.Euler();
                Debug.DrawLine(hitPoint, hitPoint + hitInfo.normal);
                var obj = Instantiate(spawnAtRayHitPrefab);
                obj.transform.position = hitPoint;
            }
            else
            {
                Debug.Log("RayInteractor.Ray did not hit any surface.");
            }
        }
    }
}

/*using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.DistanceReticles;

public class SpawnBall : MonoBehaviour
{
    [Header("Projectile Spawn")]
    public GameObject prefab;
    public float spawnSpeed = 10;

    [Header("Ray Hit Spawn")]
    public GameObject spawnAtRayHitPrefab, oculusCursor;
    public RayInteractor rightRayInteractor;

    void Update()
    {
        // Spawn projectile from controller
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            GameObject spawnedBall = Instantiate(prefab, transform.position, Quaternion.identity);
            Rigidbody spawnedBallRB = spawnedBall.GetComponent<Rigidbody>();
            spawnedBallRB.linearVelocity = transform.forward * spawnSpeed;
        }

        // Spawn object at OculusCursor 
        if (OVRInput.GetDown(OVRInput.RawButton.B) && rightRayInteractor != null)
        {
            if (rightRayInteractor.HasCandidate)
            {
                Vector3 hitPoint = oculusCursor.transform.position;
                // Vector3 hitPoint = rightRayInteractor.Ray
                // Quaternion rotation = Quaternion.LookRotation(rightRayInteractor.CandidateHit.Normal);
                Quaternion rotation = spawnAtRayHitPrefab.transform.rotation;
                Instantiate(spawnAtRayHitPrefab, hitPoint, rotation);
            }
        }
    }

}*/
