using Meta.WitAi;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private string bulletTargetTag = "Enemy";

    private void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null) return;
        if (collision.collider == null) return;
        if (!collision.gameObject.activeInHierarchy) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //despawn enemy
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
