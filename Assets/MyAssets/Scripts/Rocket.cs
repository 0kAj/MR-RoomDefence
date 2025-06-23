using System.Collections;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float geschwindigkeit = 5f;
    public float lebensdauer = 10f;

    public Transform GetZiel()
    {
        return Global.INSTANCE.GetNearestEnemy(transform.position);
    }

    void FixedUpdate()
    {

        Transform ziel = GetZiel();
        if (ziel == null)
        {
            Destroy(gameObject);
            return;
        }

        Debug.DrawLine(transform.position, ziel.position);
        // Zielrotation zur Richtung
        transform.LookAt(ziel.position);

        // Bewegung nach vorne
        Vector3 direction = ziel.position - transform.position;
        transform.position += direction * geschwindigkeit * Time.fixedDeltaTime;
    }


    private void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
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
