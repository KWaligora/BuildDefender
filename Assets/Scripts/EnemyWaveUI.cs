using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManger;

    private Camera mainCamera;
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;
    private RectTransform enemyWaveSpawnIndicator;
    private RectTransform enemyClosestPositionIndicator;

    private void Awake()
    {
        waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
        enemyWaveSpawnIndicator = transform.Find("enemyWaveSpawnIndicator").GetComponent<RectTransform>();
        enemyClosestPositionIndicator = transform.Find("enemyClosestPositionIndicator").GetComponent<RectTransform>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        SetWaveNumberText("wave 0");
        enemyWaveManger.OnWaveNumberChanged += EnemyWaveManger_OnWaveNumberChanged;
    }

    private void EnemyWaveManger_OnWaveNumberChanged(object sender, System.EventArgs e)
    {
        SetWaveNumberText("Wave " + enemyWaveManger.GetWaveNumber());
    }

    private void Update()
    {
        HandleNextWaveMassage();   

        HandleEnemyWaveSpawnPositionIndicator();
        HandleEnemyClosesPositionIndicator();
    }
    
    private void HandleNextWaveMassage()
    {
        float nextWaveSpawnTimer = enemyWaveManger.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("next wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }

    private void HandleEnemyWaveSpawnPositionIndicator()
    {
        Vector3 dirToNextSpawnPosition = (enemyWaveManger.GetSpawnPosition() - Camera.main.transform.position).normalized;

        enemyWaveSpawnIndicator.anchoredPosition = dirToNextSpawnPosition * 300f;
        enemyWaveSpawnIndicator.eulerAngles = new Vector3(0, 0, Utils.GetAngleFromVector(dirToNextSpawnPosition));

        float distanceToNextSpawnPosition = Vector3.Distance(enemyWaveManger.GetSpawnPosition(), mainCamera.transform.position);
        enemyWaveSpawnIndicator.gameObject.SetActive(distanceToNextSpawnPosition > mainCamera.orthographicSize * 1.5f);
    }

    private void HandleEnemyClosesPositionIndicator()
    {
        float targetMaxRadius = 99999f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(mainCamera.transform.position, targetMaxRadius);

        Enemy targetEnemy = null;
        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemy == null)
                    targetEnemy = enemy;
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = enemy;
                    }
                }
            }
        }

        if (targetEnemy != null)
        {
            Vector3 dirToClosestEnemy = (targetEnemy.transform.position - Camera.main.transform.position).normalized;

            enemyClosestPositionIndicator.anchoredPosition = dirToClosestEnemy * 250f;
            enemyClosestPositionIndicator.eulerAngles = new Vector3(0, 0, Utils.GetAngleFromVector(dirToClosestEnemy));

            float distanceToClosestEnemy = Vector3.Distance(targetEnemy.transform.position, mainCamera.transform.position);
            enemyClosestPositionIndicator.gameObject.SetActive(distanceToClosestEnemy > mainCamera.orthographicSize * 1.5f);
        }
        else
        {
            enemyClosestPositionIndicator.gameObject.SetActive(false);
        }
    }

    private void SetMessageText(string message)
    {
        waveMessageText.SetText(message);
    }

    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }
}
