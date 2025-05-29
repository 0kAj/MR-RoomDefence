using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(Rigidbody))]
public class AggressiveEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float updateInterval = 0.5f;

    private Rigidbody _rb;
    private GameObject _currentTarget;
    private float _timeUntilNextCheck;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _timeUntilNextCheck = 0f;
    }

    private void FixedUpdate()
    {
        _timeUntilNextCheck -= Time.fixedDeltaTime;

        if (_timeUntilNextCheck <= 0f)
        {
            _currentTarget = Global.INSTANCE.GetNearestAttackable(transform.position);
            _timeUntilNextCheck = updateInterval;
        }

        if (_currentTarget)
        {
            Vector3 direction = (_currentTarget.transform.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * (moveSpeed * Time.fixedDeltaTime);
            _rb.MovePosition(newPosition);
        }
    }
}
