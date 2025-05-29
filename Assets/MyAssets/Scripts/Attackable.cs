using UnityEngine;

public class Attackable : MonoBehaviour
{
    private void Start()
    {
        Global.INSTANCE.AddAttackable(gameObject);
    }

    public void SetAttackable()
    {
        Global.INSTANCE.AddAttackable(gameObject);
    }

    public void UnsetAttackable()
    {
        Global.INSTANCE.RemoveAttackable(gameObject);
    }
}
