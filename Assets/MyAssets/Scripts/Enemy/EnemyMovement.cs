using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(Rigidbody))]
public class AggressiveEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float updateInterval = 0.5f;

    private Rigidbody rb;
    private GameObject currentTarget;
    private float timeUntilNextCheck;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        timeUntilNextCheck = 0f;
    }

    private void FixedUpdate()
    {
        timeUntilNextCheck -= Time.fixedDeltaTime;

        if (timeUntilNextCheck <= 0f)
        {
            currentTarget = Global.INSTANCE.GetNearestAttackable(transform.position);
            timeUntilNextCheck = updateInterval;
        }

        if (currentTarget != null)
        {
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
    }
}
