using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public static int EnemiesAlive = 0;

	public Wave[] waves;

	public Transform spawnPoint;

	public float timeBetweenWaves = 5f;
	private float countdown = 2f;

	public Text waveCountdownText;

	public GameManager gameManager;

	private int waveIndex = 0;

	void Update ()
	{
		if (EnemiesAlive > 0)
		{
			return;
		}

		if (waveIndex == waves.Length)
		{
			gameManager.WinLevel();
			this.enabled = false;
		}

		if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
			return;
		}

		countdown -= Time.deltaTime;

		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

		waveCountdownText.text = string.Format("{0:00.00}", countdown);

	}

	IEnumerator SpawnWave ()
	{
		PlayerStats.Rounds++;

		Wave wave = waves[waveIndex];

		wave.count += Mathf.FloorToInt(waveIndex * wave.difficultyMultiplier); // เพิ่มจำนวนศัตรู

    	wave.rate *= 1.1f; // เพิ่มอัตราการเกิด

		EnemiesAlive = wave.count;

		for (int i = 0; i < wave.count; i++)
		{
			GameObject enemy = wave.enemies[Random.Range(0, wave.enemies.Length)];
			Enemy enemyComponent = enemy.GetComponent<Enemy>();
        	enemyComponent.startHealth *= wave.healthMultiplier;
        	enemyComponent.startSpeed *= wave.speedMultiplier;
			SpawnEnemy(enemy);
			yield return new WaitForSeconds(1f / wave.rate);
		}

		waveIndex++;
	}

	void SpawnEnemy (GameObject enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}

}
