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

    private void Awake()
    {
        waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
        enemyWaveSpawnIndicator = transform.Find("enemyWaveSpawnIndicator").GetComponent<RectTransform>();
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
        float nextWaveSpawnTimer = enemyWaveManger.GetNextWaveSpawnTimer();
        if(nextWaveSpawnTimer <= 0)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("next wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }

        Vector3 dirToNextSpawnPosition = (enemyWaveManger.GetSpawnPosition() - Camera.main.transform.position).normalized;

        enemyWaveSpawnIndicator.anchoredPosition = dirToNextSpawnPosition * 300f;
        enemyWaveSpawnIndicator.eulerAngles = new Vector3(0, 0, Utils.GetAngleFromVector(dirToNextSpawnPosition));

        float distanceToNextSpawnPosition = Vector3.Distance(enemyWaveManger.GetSpawnPosition(), mainCamera.transform.position);
        enemyWaveSpawnIndicator.gameObject.SetActive(distanceToNextSpawnPosition > mainCamera.orthographicSize * 1.5f);
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
