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

	private GameObject origin;
	private GameObject target;

	// Use this for initialization
	protected virtual void Start () 
	{
		projectileSpeed = 40;
		damage = 10;
		maxDistance = 50;

		player = GameObject.FindGameObjectWithTag ("Player");	// Get the Player

		playerDirection = player.transform.rotation * Vector3.forward;

		// RigidBody2D
		projectileMover = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Distance from player is too far
		if (Vector2.Distance (this.transform.position, origin.transform.position) > maxDistance) 
		{
			Destroy (this.gameObject);
		}
	}

	// Set origin object of the projectile
	public void setOrigin(GameObject origin) 
	{
		this.origin = origin;
	}

	// Set target object of the projectile
	public void setTarget(GameObject target) 
	{
		this.target = target;
	}

	// Fire the projectile
	public void fire() 
	{
		projectileMover = this.GetComponent<Rigidbody2D> ();
		projectileSpeed = 40f;
		Vector3 direction = origin.transform.rotation * Vector3.forward;
		// Shoot straight to your direction
		projectileMover.velocity = direction + projectileSpeed * direction.normalized;	
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
