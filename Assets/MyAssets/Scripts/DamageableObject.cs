using System.Collections;
using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int hitRadius = 2;
    [SerializeField][Tooltip("Schaden pro Gegner")] private int damageAmount = 1;
    [SerializeField][Tooltip("in Sekunden")] private int damageInterval = 1;

    private int currentHealth;
    private bool doDamageCheck = true;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthbar();

        StartCoroutine(DamageCheck());
    }


    private IEnumerator DamageCheck()
    {
        while (doDamageCheck)
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
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            EventManager.INSTANCE.TriggerGameOver(false);
            doDamageCheck = false;
        }
        UpdateHealthbar();
    }

    private void UpdateHealthbar()
    {
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(currentHealth);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }
}
