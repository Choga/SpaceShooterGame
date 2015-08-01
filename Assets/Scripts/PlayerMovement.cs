using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public GameObject bullet;			// Bullet prefab
	public GameObject explosion;		// Explosion prefab
	public GameObject healthShower;		

	public GameObject projectile;
	public float speed;					// Player speed

	private Rigidbody2D playerMover;	// Player RigidBody2D
	private Vector3 origRotation;		// Original rotation

	private float health;				// Player health

	private float timer;				// For firing rate
	private float shotTimer;
	private float firingRate;

	private int capacity;				// Player capacity 
	private int currentManifestSize;	// Player current manifest size

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
		currentManifestSize = 0;

		manifest = new string[capacity];

		// Make sure manifest starts empty of garbage
		for (int i = 0; i < capacity; i++) 
		{
			manifest[i] = null;
		}

		healthBar = healthShower.GetComponent<HealthBar> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float currSpeed = speed * Time.deltaTime;		// Current Speed

		float xInput = Input.GetAxis ("Horizontal");	// Inputs
		float yInput = Input.GetAxis("Vertical");

		playerMover.velocity = new Vector2 (xInput * currSpeed, yInput * currSpeed);	// Velocity

		// When either xInput or yInput isn't 0, recalculate rotation
		if (xInput != 0 || yInput != 0) {
			playerMover.rotation = Mathf.Atan2(xInput, yInput) * -180 / Mathf.PI;		// Rotation
		}

		// Firing rate
		timer -= Time.deltaTime;
		if(Input.GetButton ("Fire1")) {
			if(timer <= 0) {
				Instantiate (bullet);
				timer = firingRate;
			}
		}
	}

	public bool isFull() {
		return capacity <= currentManifestSize;
	}

	public void applyDamage(float damage) {
		health -= damage;
		
		if (health <= 0) {
			ExplosionHandler.createAndDestroyExplosion (this.gameObject.transform.position, explosion);
			health = 100;
		}

		healthBar.setHealth (health);
	}

	// Adds loot to player manifest if player can hold it, returns if successful
	public bool addLoot(string newLoot) {
		if (!isFull ()) {
			manifest [currentManifestSize++] = newLoot;
			string manifestString = "MANIFEST: ";
			foreach (string loot in manifest) {
				manifestString += loot + "; ";
			}
			Debug.Log (manifestString);
			return true;
		} else {
			return false;
		}
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

	// Used to clear manifest and put it in the bank
	public string[] bankManifest()
	{
		// Make sure manifest is cleared
		currentManifestSize = 0;
		// Keep the old array to return for the bank
		string[] retVal = manifest;
		// Make a new manifest so old values don't stick around
		manifest = new string[capacity];

		return retVal;
	}
}
