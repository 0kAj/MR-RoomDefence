using UnityEngine;

public class Attackable : MonoBehaviour
{
    private void Start()
    {
        Global.INSTANCE.AddAttackable(gameObject);
    }
}
