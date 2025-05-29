using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Attackable))]
public class DamageableObject : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int hitRadius = 2;
    [SerializeField][Tooltip("Schaden pro Gegner")] private int damageAmount = 1;
    [SerializeField][Tooltip("in Sekunden")] private int damageInterval = 1;

    private int _currentHealth;
    private bool _doDamageCheck = true;

    private void Start()
    {
        _currentHealth = maxHealth;
        UpdateHealthbar();

        StartCoroutine(DamageCheck());
    }


    private IEnumerator DamageCheck()
    {
        while (_doDamageCheck)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, hitRadius);

            int totalDamage = 0;

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Enemy")) // <- :D
                {
                    totalDamage += damageAmount;
                }
            }

            if (totalDamage > 0)
            {
                Damage(totalDamage);
            }

            yield return new WaitForSeconds(damageInterval);
        }
    }


    private void Damage(int amount)
    {
        _currentHealth -= amount;
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
            EventManager.Instance.TriggerGameOver(false);
            GetComponent<Attackable>().UnsetAttackable();
            _doDamageCheck = false;
        }
        UpdateHealthbar();
    }

    private void UpdateHealthbar()
    {
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(_currentHealth);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }
}
