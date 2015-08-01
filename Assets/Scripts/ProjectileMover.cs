using UnityEngine;
using System.Collections;

public class ProjectileMover : MonoBehaviour {
	 
	private GameObject player;

	private Rigidbody2D projectileMover;

	private float speed;
	public GameObject explosion;
	private float damage;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player");

		this.transform.position = player.transform.position + new Vector3 (0, 2, 0);

		projectileMover = this.GetComponent<Rigidbody2D> ();

		projectileMover.velocity = new Vector2 (0, 10);

		speed = 10;
		damage = 10;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector2.Distance (this.transform.position, player.transform.position) > 50) 
		{
			Destroy (this.gameObject);
		}
		this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + speed * Time.deltaTime);
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
