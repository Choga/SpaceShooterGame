using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour 
{
	public GameObject explosion;
	public float speed;
	
	private Rigidbody2D theEnemyMover;
	
	private Transform playerTransform; // Target Object to follow
	
	private Vector3 directionOfPlayer;
	
	private Vector3 origRotation;

	private float health;
	
	// Use this for initialization
	void Start () 
	{
		theEnemyMover = this.GetComponent<Rigidbody2D> ();
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		origRotation = this.transform.eulerAngles;

		health = 70;
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool challenged = true;
		
		if (challenged) {
			directionOfPlayer = playerTransform.position - this.transform.position;
			directionOfPlayer = directionOfPlayer.normalized;    // Get Direction to Move Towards
			
			Vector3 worldUp = new Vector3(0f, 0f, 90f);        // World's "up" direction
			
			float playerX = playerTransform.position.x;
			float playerY = playerTransform.position.y;
			float playerZ = playerTransform.position.z;
			Quaternion playerRotation = playerTransform.rotation;
			
			float enemyX = this.transform.position.x;
			float enemyY = this.transform.position.y;
			float enemyZ = this.transform.position.z;
			Quaternion enemyRotation = this.transform.rotation;                    
			
			
			this.transform.Translate (directionOfPlayer * speed, Space.World); // Move enemey to player
			this.transform.LookAt (playerTransform, worldUp);        // Points torwards Player
		}
	}

	public void applyDamage(float damage) {
		health -= damage;
		
		if (health <= 0) {
			Destroy (this.gameObject);
			ExplosionHandler.createAndDestroyExplosion (this.gameObject.transform.position, explosion);
		}
		
	}
}