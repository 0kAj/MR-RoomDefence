using UnityEngine;

public class IAmEnemy : MonoBehaviour
{
    void Awake()
    {
        Global.INSTANCE.AddEnemy(transform);
    }
}
