using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global INSTANCE { get; private set; }

    private List<GameObject> attackables = new List<GameObject>();

    private void Awake()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
            return;
        }

        INSTANCE = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddAttackable(GameObject attackable)
    {
        if (!attackables.Contains(attackable))
            attackables.Add(attackable);
    }

    public void RemoveAttackable(GameObject attackable)
    {
        if (attackables.Contains(attackable))
            attackables.Remove(attackable);
    }

    public GameObject GetNearestAttackable(Vector3 fromPosition)
    {
        GameObject nearest = null;
        float shortestDistance = Mathf.Infinity;

        foreach (var obj in attackables)
        {
            if (!obj) continue;

            float dist = Vector3.Distance(fromPosition, obj.transform.position);
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                nearest = obj;
            }
        }

        return nearest;
    }
}
