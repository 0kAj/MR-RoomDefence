using UnityEngine;

public class UIFollowHand : MonoBehaviour
{
    public Transform handTransform;
    public Vector3 offset = new Vector3(0, 0.1f, 0.1f);

    void LateUpdate()
    {
        if (handTransform != null)
        {
            transform.position = handTransform.position + handTransform.rotation * offset;
            transform.rotation = handTransform.rotation;
        }
    }
}