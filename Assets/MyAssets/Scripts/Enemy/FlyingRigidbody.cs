using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlyingRigidbody : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float distanceToFloor = 1.5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (IsNearGround())
        {
            Jump();
        }
    }

    private bool IsNearGround()
    {
        Debug.DrawRay(transform.position, Vector3.down * distanceToFloor, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, distanceToFloor);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reset Y
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}