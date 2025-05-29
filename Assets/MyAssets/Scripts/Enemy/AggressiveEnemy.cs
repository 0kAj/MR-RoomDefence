using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AggressiveEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float updateInterval = 0.5f;

    [Header("Jumping")]
    [SerializeField] private bool doRandomJump = false;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] [Tooltip("minTimeToJump * updateInterval")] private float minTimeToJump = 5f;
    [SerializeField] [Tooltip("maxTimeToJump * updateInterval")] private float maxTimeToJump = 10f;
    

    private Rigidbody _rb;
    private GameObject _currentTarget;
    private float _timeUntilNextCheck;
    
    private float _timeUntilNextJump;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _timeUntilNextCheck = 0f;
        _timeUntilNextJump = Random.Range(minTimeToJump, maxTimeToJump);
    }

    private void FixedUpdate()
    {
        _timeUntilNextCheck -= Time.fixedDeltaTime;

        if (_timeUntilNextCheck <= 0f)
        {
            _currentTarget = Global.INSTANCE.GetNearestAttackable(transform.position);
            _timeUntilNextCheck = updateInterval;
        }

        _timeUntilNextJump -= Time.fixedDeltaTime;

        if (_timeUntilNextJump <= 0f)
        {
            // Jump
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _timeUntilNextJump = Random.Range(minTimeToJump, maxTimeToJump);
        }

        if (_currentTarget)
        {
            Vector3 direction = (_currentTarget.transform.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * (moveSpeed * Time.fixedDeltaTime);
            _rb.MovePosition(newPosition);
        }
    }
}
