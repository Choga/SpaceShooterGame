using UnityEngine;
using System.Collections;

public class ProjectileMover : MonoBehaviour {
	 
	private GameObject player;

	private Rigidbody2D projectileMover;

	private float projectileSpeed;
	public GameObject explosion;
	private float damage;
	private float maxDistance;

	private Vector3 playerDirection;

	// Use this for initialization
	void Start () 
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
	if (Vector2.Distance (this.transform.position, player.transform.position) > maxDistance) 
		{
			Destroy (this.gameObject);
		}
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		explosion.transform.position = this.gameObject.transform.position;
		ExplosionHandler.createAndDestroyExplosion (this.gameObject.transform.position, explosion);
		if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player") {
			collision.gameObject.BroadcastMessage ("applyDamage", damage);
		}
		Destroy (this.gameObject);
	}
}
