using System;
using UnityEngine;

[Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public float duration;
    public int count;

    public Wave(GameObject enemyPrefab, float duration, int count)
    {
        this.enemyPrefab = enemyPrefab;
        this.duration = duration;
        this.count = count;
    }
}
