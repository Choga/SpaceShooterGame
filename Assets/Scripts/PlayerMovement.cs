using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public GameObject bullet;
	public GameObject explosion;
	public GameObject healthShower;

	public GameObject projectile;
	public float speed;

	private static Rigidbody2D playerMover;
	private Vector3 origRotation;

	private float health;
	private float timer;
	private float shotTimer;
	private float firingRate;

	private int capacity;
	private int currentSize;

	private string[] manifest;
	private HealthBar healthBar;


	// Use this for initialization
	void Start () 
	{
		playerMover = this.GetComponent<Rigidbody2D> ();

		origRotation = this.transform.eulerAngles;
		health = 100;
		timer = 0;
		firingRate = 0.5f;
		capacity = 2;
		currentSize = 0;

		manifest = new string[capacity];

		healthBar = healthShower.GetComponent<HealthBar> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float currSpeed = speed * Time.deltaTime;

		float xInput = Input.GetAxis ("Horizontal");
		float yInput = Input.GetAxis("Vertical");

		playerMover.velocity = new Vector2 (xInput * currSpeed, yInput * currSpeed);	// Velocity

		// When either x or y isn't 0
		if (xInput != 0 || yInput != 0) {
			playerMover.rotation = Mathf.Atan2(xInput, yInput) * -180 / Mathf.PI;		// Rotation
		}

		Debug.Log (playerMover.rotation);

		timer -= Time.deltaTime;
		if(Input.GetButton ("Fire1")) {
			if(timer <= 0) {
				Instantiate (bullet);
				timer = firingRate;
			}
		}
	}

	// Returns the RigidBody2D
	public static Rigidbody2D getPlayerMover() 
	{
		return playerMover;
	}

	public bool isFull() {
		return capacity == currentSize;
	}

	public void applyDamage(float damage) {
		health -= damage;
		
		if (health <= 0) {
			ExplosionHandler.createAndDestroyExplosion (this.gameObject.transform.position, explosion);
			health = 100;
		}

		healthBar.setHealth (health);
	}

	public void addLoot(string newLoot) {
		manifest[currentSize++] = newLoot;
		string manifestString = "MANIFEST: ";
		foreach (string loot in manifest) {
			manifestString += loot + "; ";
		}
		Debug.Log (manifestString);
	}

	public float getHealth()
	{
		return health;
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy") {
			collision.gameObject.BroadcastMessage ("applyDamage", .1f);
		}
	}
}
