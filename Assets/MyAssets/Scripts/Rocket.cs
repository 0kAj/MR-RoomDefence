using UnityEngine;

public class Rakete : MonoBehaviour
{
    public float geschwindigkeit = 25f;
    public float rotationsgeschwindigkeit = 10f;
    public float lebensdauer = 10f;
    private Transform ziel;

    public Vector3 raketenOffsetRotation = new Vector3(90, 0, 0); // Optional: falls Modell falsch ausgerichtet ist

    public void SetZiel(Transform neuesZiel)
    {
        ziel = neuesZiel;
        Destroy(gameObject, lebensdauer);
    }

    void FixedUpdate()
    {
        if (ziel == null)
        {
            Destroy(gameObject);
            return;
        }

        // Richtung zum Ziel berechnen
        Vector3 richtung = (ziel.position - transform.position).normalized;

        // Zielrotation zur Richtung
        Quaternion zielRotation = Quaternion.LookRotation(richtung);

        // Glatte Rotation zur Zielrichtung
        transform.rotation = Quaternion.Lerp(transform.rotation, zielRotation, rotationsgeschwindigkeit * Time.fixedDeltaTime);

        // Wenn nötig: Korrigiere Modellrotation (z. B. Y-Up auf Z-Forward)
        transform.rotation *= Quaternion.Euler(raketenOffsetRotation);

        // Bewegung nach vorne
        transform.position += transform.forward * geschwindigkeit * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ziel != null && other.transform == ziel)
        {
            Debug.Log("Ziel getroffen: " + ziel.name);

            // Ziel zerstören
            Destroy(ziel.gameObject);

            // Rakete zerstören
            Destroy(gameObject);
        }
    }
}
