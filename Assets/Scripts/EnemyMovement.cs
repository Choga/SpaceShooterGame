using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject loot;
	public float speed;
	
	private Rigidbody2D theEnemyMover;
	
	private Transform playerTransform; // Target Object to follow
	
	private Vector3 directionOfPlayer;

	private Vector3 origRotation;
	
	private float health;

	private int size;
	private int shipClass;
	bool challenged = false;					// Flag for enemy aggro

	Vector3 worldUp = new Vector3(0f, 0f, 90f);	// World's "up" direction



	// Use this for initialization
	void Start () 
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		origRotation = this.transform.eulerAngles;
		health = 70;
		size = 5;
		shipClass = 6;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float distance = Vector3.Distance (this.transform.position, playerTransform.transform.position);

		if (distance < 5f) 
			challenged = true;
		else if (distance > 10f) 
			challenged = false;

		// Actions when enemy is challenged
		if (challenged) {
			directionOfPlayer = (playerTransform.position - this.transform.position).normalized;	// Get direction to move towards			

			float playerX = playerTransform.position.x;
			float playerY = playerTransform.position.y;
			float playerZ = playerTransform.position.z;
			Quaternion playerRotation = playerTransform.rotation;
			
			float enemyX = this.transform.position.x;
			float enemyY = this.transform.position.y;
			float enemyZ = this.transform.position.z;
			Quaternion enemyRotation = this.transform.rotation;                    
			
			
			this.transform.Translate (directionOfPlayer * speed, Space.World); 	// Move enemey to player
			this.transform.LookAt (playerTransform, worldUp);        			// Points torwards Player
		} else if (!challenged) {

			float speed = 5f;
			Vector2 vel;

			vel = Random.insideUnitCircle * speed;
			this.transform.Translate (vel * Time.deltaTime, Space.World);
		}
	}
	
	public void applyDamage(float damage) {
		health -= damage;

		if (health <= 0) {
			Destroy (this.gameObject);
			ExplosionHandler.createAndDestroyExplosion (this.gameObject.transform.position, explosion);
			spawnLoot ();
		}
		
	}

	void spawnLoot() {
		Vector3 position = this.gameObject.transform.position;
		float enemyMeshSizeX = this.gameObject.GetComponent<MeshFilter> ().mesh.bounds.size.x;
		float deltaX = size/(enemyMeshSizeX * this.gameObject.transform.localScale.x);
		position.x = position.x - (enemyMeshSizeX);
		for (int i = 0; i < size; i++) {
			LootHandler lootHandler = (LootHandler) (((GameObject)Instantiate(loot, position, Quaternion.identity)).GetComponent ("LootHandler"));
			lootHandler.setClass (shipClass);
			position.x = position.x + deltaX;
		}
	}
}