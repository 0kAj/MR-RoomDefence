using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlyingRigidbody : MonoBehaviour
{
    [SerializeField] private float distanceToFloor = 1.5f;

    [Header("Random Floating")]
    [SerializeField] private float floatAmplitude = 0.5f; // Höhe der Schwankung
    [SerializeField] private float floatFrequency = 1.5f; // Geschwindigkeit der Schwankung

    private Rigidbody rb;
    private float timeOffset;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        timeOffset = Random.Range(0f, 100f); // verhindert Synchronisation mehrerer Objekte
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        bool hit = Physics.Raycast(ray, out RaycastHit hitInfo);

        Color rayColor = Color.red;

        if (hit)
        {
            rayColor = Color.green;
            Debug.DrawLine(ray.origin, hitInfo.point, rayColor);

            float currentDistance = hitInfo.distance;

            float targetDistance = distanceToFloor + Mathf.Sin((Time.time + timeOffset) * floatFrequency) * floatAmplitude;

            float delta = targetDistance - currentDistance;

            if (Mathf.Abs(delta) > 0.01f)
            {
                float requiredVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * Mathf.Abs(delta));
                requiredVelocity *= Mathf.Sign(delta);

                Vector3 velocity = rb.linearVelocity;
                velocity.y = requiredVelocity;
                rb.linearVelocity = velocity;
            }
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * distanceToFloor, rayColor);
        }
    }
}