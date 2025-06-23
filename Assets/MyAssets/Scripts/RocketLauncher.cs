using UnityEngine;
using System.Collections.Generic;

public class RaketenSilo : MonoBehaviour
{
    public GameObject raketenPrefab;         // Prefab der Rakete
    public Transform feuerpunkt;             // Ort, von dem die Rakete abgefeuert wird
    public float reichweite = 30f;           // Angriffsreichweite
    public float feuerrate = 2f;             // Zeit zwischen Abschüssen
    private float naechsterSchuss = 0f;

    void Update()
    {
        GameObject ziel = FindeNaechstesZiel();

        if (ziel != null && Time.time >= naechsterSchuss)
        {
            SchiesseRakete(ziel);
            naechsterSchuss = Time.time + feuerrate;
        }
    }

    GameObject FindeNaechstesZiel()
    {
        GameObject[] gegner = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject naechster = null;
        float kuerzesteEntfernung = Mathf.Infinity;

        foreach (GameObject feind in gegner)
        {
            float entfernung = Vector3.Distance(transform.position, feind.transform.position);
            if (entfernung < kuerzesteEntfernung && entfernung <= reichweite)
            {
                kuerzesteEntfernung = entfernung;
                naechster = feind;
            }
        }

        return naechster;
    }

    void SchiesseRakete(GameObject ziel)
    {
        GameObject rakete = Instantiate(raketenPrefab, feuerpunkt.position, feuerpunkt.rotation);
        Rakete raketenScript = rakete.GetComponent<Rakete>();
        if (raketenScript != null)
        {
            Debug.Log("Ziel gesetzt: " + ziel.name);
            raketenScript.SetZiel(ziel.transform);
        }
        else
        {
            Debug.LogWarning("Rakete hat kein Rakete-Skript!");
        }
    }

}

