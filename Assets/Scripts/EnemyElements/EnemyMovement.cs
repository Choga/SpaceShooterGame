using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour 
{
	public float speed;							// Speed of enemy
	public GameObject explosion;				// Explosion prefab 
	
	private Rigidbody2D theEnemyMover;			// Enemy RigidBody2D
	
	private Transform playerTransform; 			// Target Object to follow
	
	private Vector3 directionOfPlayer;			// Direction of player

	private Vector3 origRotation;				// Original rotation
	
	private float health;

	private float minChallengeDistance = 10f;	// Aggro distance
	private float maxChallengeDistance = 15f;	// Max aggro distance

	private int size;							// Size
	private int shipClass;						// Ship class
	bool challenged = false;					// Flag for enemy aggro

	Vector3 worldUp = new Vector3(0f, 0f, 90f);	// World's "up" direction



	// Use this for initialization
	void Start () 
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		origRotation = this.transform.eulerAngles;
		health = 10f;
		size = 5;
		shipClass = 6;
		speed = 5f;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		// Distance of enemy from player
		float distance = Vector3.Distance (this.transform.position, playerTransform.transform.position);

		if (distance < minChallengeDistance) 
			challenged = true;
		else if (distance > maxChallengeDistance) 
			challenged = false;

		// Actions when enemy is challenged
		if (challenged) {
			directionOfPlayer = (playerTransform.position - this.transform.position).normalized;	// Get direction to move towards			

			this.transform.Translate (directionOfPlayer * speed * Time.deltaTime, Space.World); 	// Move enemey to player
			this.transform.LookAt (playerTransform, worldUp);	      			// Points torwards Player
		} else if (!challenged) {

			// Random idle animations 
		}
	}

	// Apply damage, destroy if appropriate 
	public void applyDamage(float damage) 
	{
		health -= damage;

		if (health <= 0) {
			Destroy (this.gameObject);
			ExplosionHandler.createAndDestroyExplosion (this.gameObject.transform.position, explosion);
			spawnLoot ();							// Looot
			EnemyManager.decrementEnemyCount(1);	// Decrement the count of enemies in the manager
		}
		
	}
	
	public void OnCollisionEnter2D(Collision2D collision)
	{
		// If colliding with player
		if (collision.gameObject.tag == "Player") {
			collision.gameObject.BroadcastMessage ("applyDamage", 0.1f);	
		}
	}

	void spawnLoot() 
	{
		Vector3 position = this.gameObject.transform.position;

		float enemyMeshSizeX = this.gameObject.GetComponent<MeshFilter> ().mesh.bounds.size.x;
		float deltaX = size / (enemyMeshSizeX * this.gameObject.transform.localScale.x);

		position.x = position.x - (enemyMeshSizeX);

		float randomRange = deltaX;
		for (int i = 0; i < size; i++) {

			// Random position for the loot defined by these
			float current1 = (Random.Range (-1*randomRange, randomRange));
			float current2 = (Random.Range (-1*randomRange, randomRange));

			// Getting random loot from our loot handler
			GameObject loot = LootHandler.getRandomLoot(shipClass);
			// Spawning loot on the screen at position altered by random value
			Instantiate(loot, position + new Vector3(current1, current2, 0) , Quaternion.identity);
		}
	}
}