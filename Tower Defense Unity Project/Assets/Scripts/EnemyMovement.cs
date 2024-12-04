using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

	private Transform target;
	private int wavepointIndex = 0;

	private Enemy enemy;

	private bool isIdle = false; // ค่าเริ่มต้นคือไม่อยู่ในสถานะ idle

	private float idleTime = 0f; // เวลา idle เริ่มต้น


	void Start()
	{
		enemy = GetComponent<Enemy>();

		target = Waypoints.points[0];
	}

	void Update()
	{
		if (isIdle) 
		{
			idleTime -= Time.deltaTime;
        
			if (idleTime <= 0) 
			{
            	isIdle = false;
        	}
        return;
    	}


		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWaypoint();
		}

		enemy.speed = enemy.startSpeed;
	}

	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1)
		{
			EndPath();
			return;
		}

		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];

		// ตั้งสถานะ idle พร้อมกำหนดเวลา (ตัวอย่าง)
        isIdle = true;
        idleTime = Random.Range(0.1f, 3f); // หยุดพักแบบสุ่มระหว่าง 0.1-3 วินาที
	}

	void EndPath()
	{
		PlayerStats.Lives--;
		WaveSpawner.EnemiesAlive--;
		Destroy(gameObject);
	}

}
