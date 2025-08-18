using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    private int _currentWaveIndex = 0;

    private bool _isSpawning = false;
    private Wave[] _waves;

    [Header("Wave Count")]
    [SerializeField] private int minWaveCount = 1;
    [SerializeField] private int maxWaveCount = 2;

    [Header("Wave Duration")]
    [SerializeField] private int minWaveDuration = 2;
    [SerializeField] private int maxWaveDuration = 5;

    [Header("Wave Enemy Count")]
    [SerializeField] private int minWaveEnemyCount = 2;
    [SerializeField] private int maxWaveEnemyCount = 5;

    private Coroutine _waveCoroutine;

    private enum WaveSpawnerState
    {
        SPAWNING,
        WAITING,
        FINISHED
    }

    private WaveSpawnerState _state = WaveSpawnerState.WAITING;

    private void Awake()
    {
        GenerateWaves();
    }

    void Start()
    {
        EventManager.Instance.StartGameListener += StartWaves;
        EventManager.Instance.GameOverListener += StopWaves;
    }

    private void OnDestroy()
    {
        EventManager.Instance.StartGameListener -= StartWaves;
        EventManager.Instance.GameOverListener -= StopWaves;
    }


    private void StartWaves()
    {
        if (_waveCoroutine != null)
        {
            StopCoroutine(_waveCoroutine);
        }

        _isSpawning = true;
        _currentWaveIndex = 0;
        Debug.Log("Starting wave spawning...");
        _waveCoroutine = StartCoroutine(SpawnWaves());
    }

    private void StopWaves()
    {
        if (_waveCoroutine != null)
        {
            StopCoroutine(_waveCoroutine);
            _waveCoroutine = null;
        }

        _isSpawning = false;
        Debug.Log("Wave spawning stopped.");
    }

    private IEnumerator SpawnWaves()
    {
        while (_isSpawning)
        {
            _state = WaveSpawnerState.SPAWNING;

            while (_currentWaveIndex < _waves.Length)
            {
                Wave wave = _waves[_currentWaveIndex];
                Debug.Log($"Spawning Wave {_currentWaveIndex + 1}");

                for (int i = 0; i < wave.count; i++)
                {
                    GameObject gameObj = Instantiate(
                        wave.enemyPrefab,
                        GetRandomSpawnPoint().position,
                        Quaternion.identity
                    );
                    gameObj.transform.parent = transform;

                    yield return new WaitForSeconds(wave.duration / wave.count);
                }

                _currentWaveIndex++;
                yield return new WaitForSeconds(1f); // Delay between waves
            }

            Debug.Log("All waves spawned. Restarting...");

            _currentWaveIndex = 0;
            yield return new WaitForSeconds(2f); // Optional delay before loop restarts
        }

        _state = WaveSpawnerState.FINISHED;
    }



    private Transform GetRandomSpawnPoint()
    {
        List<Transform> spawnPoints = Global.INSTANCE.GetSpawnPoints();
        if (spawnPoints.Count == 0)
        {
            throw new System.ArgumentNullException("No spawn points assigned to WaveSpawner.");
        }

        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }

    private GameObject GetRandomEnemyPrefab()
    {
        if (enemyPrefabs.Length == 0)
        {
            throw new System.ArgumentNullException("No enemyPrefabs assigned to WaveSpawner.");
        }

        return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
    }

    private void GenerateWaves()
    {
        int waveCount = Random.Range(minWaveCount, maxWaveCount + 1);
        _waves = new Wave[waveCount];

        for (int i = 0; i < waveCount; i++)
        {
            int duration = Random.Range(minWaveDuration, maxWaveDuration + 1);
            int enemyCount = Random.Range(minWaveEnemyCount, maxWaveEnemyCount + 1);

            _waves[i] = new Wave(GetRandomEnemyPrefab(), duration, enemyCount);
        }
    }
}
