using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }


    private State state;
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private int remainingEnemySpawnAmount;
    Vector3 spawnPosition;

    private void Start()
    {
        state = State.WaitingToSpawnNextWave;
        nextWaveSpawnTimer = 3f;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer < 0f)
                {
                    SpawnWave();
                }
                break;

            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer < 0f)
                    {
                        nextEnemySpawnTimer = Random.Range(0f, .2f);
                        Enemy.Create(spawnPosition + Utils.GetRandomDir() * Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;

                        if (remainingEnemySpawnAmount <= 0)
                            state = State.WaitingToSpawnNextWave;
                    }
                }
                break;
        }            
    }

    private void SpawnWave()
    {
        spawnPosition = new Vector3(40, 0);
        nextWaveSpawnTimer = 10f;
        remainingEnemySpawnAmount = 10;
        state = State.SpawningWave;
    }
}
