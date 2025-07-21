using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(0, .5f, 0);

    void Start()
    {
        Transform t = transform;
        t.position += offset;
        Global.INSTANCE.AddSpawnPoint(t);
    }
}
