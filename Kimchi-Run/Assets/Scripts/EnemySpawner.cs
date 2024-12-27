using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    public float eMinSpawnDelay;
    public float eMaxSpawnDelay;
    public float difficultyIncreaseInterval = 5f; // 난이도 증가 간격 (초 단위)
    public float spawnDelayDecreaseAmount = 0.5f; // 난이도 증가 시 지연 시간 감소량

    [Header("References")]
    public GameObject[] enemyObjects; // 생성할 적 오브젝트들을 담을 배열

    private float nextDifficultyIncreaseTime;

    void OnEnable()
    {
        Invoke("Spawn", Random.Range(eMinSpawnDelay, eMaxSpawnDelay));
        nextDifficultyIncreaseTime = Time.time + difficultyIncreaseInterval;
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void Update()
    {
        if (Time.time >= nextDifficultyIncreaseTime)
        {
            IncreaseDifficulty();
            nextDifficultyIncreaseTime = Time.time + difficultyIncreaseInterval;
        }
    }

    void Spawn()
    {
        GameObject randomEnemy = enemyObjects[Random.Range(0, enemyObjects.Length)];
        Instantiate(randomEnemy, transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(eMinSpawnDelay, eMaxSpawnDelay));
    }

    void IncreaseDifficulty()
    {
        eMinSpawnDelay = Mathf.Max(0.1f, eMinSpawnDelay - spawnDelayDecreaseAmount);
        eMaxSpawnDelay = Mathf.Max(0.2f, eMaxSpawnDelay - spawnDelayDecreaseAmount);
    }
}