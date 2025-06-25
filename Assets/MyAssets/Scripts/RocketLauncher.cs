using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RocketLauncher : MonoBehaviour
{
    public GameObject raketenPrefab;         // Prefab der Rakete
    public Transform feuerpunkt;             // Ort, von dem die Rakete abgefeuert wird
    public float shootInterval = 2f;             // Zeit zwischen Abschüssen
    [SerializeField][Tooltip("In Sekunden")] private float naechsterSchuss = 5f;

    void Start()
    {
        EventManager.Instance.StartGameListener += StartWave;
    }

    void StartWave()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootInterval);
        Instantiate(raketenPrefab, feuerpunkt.position, feuerpunkt.rotation);
        yield return Shoot();
    }
}

