using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemy;                // The enemy prefab to be spawned.
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	private float spawnShell = 10f;			// Shell width around player where enemies can spawn
	private static int maxEnemyCount = 10;	// Max amount of enemies
	private static int enemyCount = 0;			// Current amount of enemies
	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn ()
	{
		if (enemyCount < maxEnemyCount) 
		{
			// Create an instance of the enemy prefab at the randomly selected spawn section, at least 15 away 
			Vector3 randomPoint = (Random.insideUnitCircle * spawnShell) + (Random.insideUnitCircle * 15);
			// Random rotation for enemy ship
			Quaternion randRotation = Random.rotation;
			Instantiate (enemy, randomPoint, randRotation);
			incrementEnemyCount (1);
		}
	}

	public static void decrementEnemyCount(int num) 
	{
		enemyCount -= num;
		if (enemyCount < 0)
			enemyCount = 0;
	}
	public static void incrementEnemyCount(int num) 
	{
		enemyCount += num;
		if (enemyCount > maxEnemyCount)
			enemyCount = maxEnemyCount;
	}
}