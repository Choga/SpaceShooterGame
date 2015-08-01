using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemy;                // The enemy prefab to be spawned.
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	private float spawnShell = 10f;			// Shell width around player where enemies can spawn
	
	
	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	
	void Spawn ()
	{
		
		// Create an instance of the enemy prefab at the randomly selected spawn section, at least 15 away 
		Vector3 randomPoint = (Random.insideUnitCircle * spawnShell) + (Random.insideUnitCircle * 15);
		// Random rotation for enemy ship
		Quaternion randRotation = Random.rotation;
		Instantiate (enemy, randomPoint, randRotation);
		
	}
}