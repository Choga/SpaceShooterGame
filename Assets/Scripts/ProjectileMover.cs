using UnityEngine;
using System.Collections;

public class ProjectileMover : MonoBehaviour {
	 
	private GameObject player;

	private Rigidbody2D projectileMover;

	private float projectileSpeed;
	public GameObject explosion;
	private float damage;
	private float maxDistance;

	private float playerRotationX;
	private float playerRotationY;

	// Use this for initialization
	void Start () 
	{

		projectileSpeed = 20;
		damage = 10;
		maxDistance = 50;

		player = GameObject.FindGameObjectWithTag ("Player");	// Get the Player

		this.transform.position = player.transform.position + new Vector3 (0, 0, 0); // Initial position for the projectile

		projectileMover = this.GetComponent<Rigidbody2D> ();

		Vector2 playerVelocity = player.GetComponent<Rigidbody2D>().velocity;

		if (playerVelocity.Equals (new Vector2 (0, 0))) 
		{ 	
			// If player is not moving
			projectileMover.velocity = new Vector2 (0, projectileSpeed);
		} 
		else 
		{
			// Shoot direction of the player velocity
			projectileMover.velocity = playerVelocity + projectileSpeed * playerVelocity.normalized;	
		}


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
