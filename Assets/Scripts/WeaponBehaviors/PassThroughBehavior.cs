using UnityEngine;
using System.Collections;

public class PassThroughBehavior : ProjectileMover {
	private int maxPassThroughs;
	private int damageBoost;
	private int speedReduction;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		damageBoost = 10;
		speedReduction = 2;
		maxPassThroughs = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnCollisionEnter2D(Collision2D collision) {
		// Explosion location where player is
		explosion.transform.position = this.gameObject.transform.position;
		
		// Pass to ExplosionHandler to create explosion
		ExplosionHandler.createAndDestroyExplosion (this.gameObject.transform.position, explosion);
		
		// Apply the projectile damage to the object
		if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player") {
			collision.gameObject.BroadcastMessage ("applyDamage", damage);
			if(maxPassThroughs > 0) {
				damage += damageBoost;
				projectileSpeed -= speedReduction;
			} else {
				Destroy (this.gameObject);	// Destroy projectile after collision
			}
		}
		

	}
}
