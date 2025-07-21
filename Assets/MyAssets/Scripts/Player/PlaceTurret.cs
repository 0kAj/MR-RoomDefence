using UnityEngine;
using Oculus.Interaction;

public class PlaceTurret : MonoBehaviour
{
    [Header("Ray Hit Spawn")]
    public GameObject spawnAtRayHitPrefab;
    public GameObject previewObjPrefab;
    public RayInteractor rightRayInteractor;
    public float raycastDistance = 10f;

    private GameObject previewObj = null;

    void Update()
    {
        if (previewObj == null)
        {
            previewObj = Instantiate(previewObjPrefab);
        }
        print(previewObj);
        Ray ray = rightRayInteractor.Ray;
        LayerMask layerMask = LayerMask.GetMask("Default", "Environment");
        if (Physics.Raycast(ray, out RaycastHit hitInfo, raycastDistance, layerMask))
        {
            Vector3 hitPoint = hitInfo.point;
            Debug.DrawLine(hitPoint, hitPoint + hitInfo.normal);
            previewObj.transform.position = hitPoint;
        }



        // Spawn prefab at raycast hit location (nicht unbedingt beim OculusCursor)
        if (OVRInput.GetDown(OVRInput.RawButton.B) && rightRayInteractor != null)
        {
            Vector3 hitPoint = hitInfo.point;
            Debug.DrawLine(hitPoint, hitPoint + hitInfo.normal);
            var obj = Instantiate(spawnAtRayHitPrefab);
            obj.transform.position = hitPoint;
        }
    }

}
