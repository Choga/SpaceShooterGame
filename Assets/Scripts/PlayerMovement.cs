using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public GameObject bullet;
	public GameObject explosion;

	public GameObject projectile;
	public float speed;

	private Rigidbody2D playerMover;
	private Vector3 origRotation;

	private float health;
	private float timer;
	private float shotTimer;
	private float firingRate;


	// Use this for initialization
	void Start () 
	{
		playerMover = this.GetComponent<Rigidbody2D> ();

		origRotation = this.transform.eulerAngles;
		health = 70;
		timer = 0;
		firingRate = 0.5f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float currSpeed = speed * Time.deltaTime;

		float xInput = Input.GetAxis ("Horizontal");
		float yInput = Input.GetAxis("Vertical");

		playerMover.velocity = new Vector2 (xInput * currSpeed, yInput * currSpeed);

		//this.transform.eulerAngles = new Vector3 (origRotation.x, origRotation.y, Mathf.Atan2(xInput, yInput) * 180 / Mathf.PI);

		timer -= Time.deltaTime;
		if(Input.GetButton ("Fire1")) {
			if(timer <= 0) {
				Instantiate (bullet);
				timer = firingRate;
			}
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
