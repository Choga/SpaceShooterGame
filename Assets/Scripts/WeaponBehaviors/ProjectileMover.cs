using UnityEngine;
using System.Collections;

public class ProjectileMover : MonoBehaviour {
	 
	private GameObject player;				// Player GameObject

	private Rigidbody2D projectileMover;	// RigidBody2D for projectile

	protected float projectileSpeed;			// Speed for projectile

	public GameObject explosion;			// Explosion prefab

	protected float damage;					// Damage the projectile does

	private float maxDistance;				// Maximum distance projectile can be from player

	private Vector3 playerDirection;		// Player's direction

	// Use this for initialization
	protected virtual void Start () 
	{
		projectileSpeed = 40;
		damage = 10;
		maxDistance = 50;

		player = GameObject.FindGameObjectWithTag ("Player");	// Get the Player

		playerDirection = player.transform.rotation * Vector3.forward;

		// Initial position for the projectile
		this.transform.position = player.transform.position + playerDirection; 

		// RigidBody2D
		projectileMover = this.GetComponent<Rigidbody2D> ();

		// Velocity according to player's rotation
		projectileMover.velocity = playerDirection + projectileSpeed * playerDirection.normalized;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Distance from player is too far
		if (Vector2.Distance (this.transform.position, player.transform.position) > maxDistance) 
		{
			Destroy (this.gameObject);
		}
	}

	public virtual void OnCollisionEnter2D(Collision2D collision)
	{
		// Explosion location where player is
		explosion.transform.position = this.gameObject.transform.position;

		// Pass to ExplosionHandler to create explosion
		ExplosionHandler.createAndDestroyExplosion (this.gameObject.transform.position, explosion);

		// Apply the projectile damage to the object
		if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player") {
			collision.gameObject.BroadcastMessage ("applyDamage", damage);
		}

		Destroy (this.gameObject);	// Destroy projectile after collision
	}
}
