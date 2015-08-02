using UnityEngine;
using System.Collections;

public class PeaShooterBehavior : ProjectileMover {

	//Template for upgrades - Floats store damage/speed boost; Boolean stores whether or not upgrade is active
	private float damageUpgradeFactor;
	private float speedUpgradeFactor;
	
	public static bool upgradeDamage;
	public static bool upgradeSpeed;
	
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();

		damage = 10;

		damageUpgradeFactor = 10;
		speedUpgradeFactor = 10;
		
		if (upgradeDamage) {
			damage *= damageUpgradeFactor;
		}
		if (upgradeSpeed) {
			projectileSpeed *= speedUpgradeFactor;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public override void OnCollisionEnter2D(Collision2D collision) {
		base.OnCollisionEnter2D (collision);
	}
}
