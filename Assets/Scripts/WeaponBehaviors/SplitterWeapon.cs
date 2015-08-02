using UnityEngine;
using System.Collections;

public class SplitterWeapon : ProjectileMover
{
	private int splitAmount = 4;
	private float scaleFactor = .75f;

	private float minSize = .25f;

	public override void OnCollisionEnter2D (Collision2D collision)
	{
		base.OnCollisionEnter2D (collision);
		// Make the bullet smaller before multiplying
		this.transform.localScale *= scaleFactor;
		// If this object is bigger than the min size for a bullet
		if (this.gameObject.transform.localScale.x > minSize) {
			// Split into <splitAmount> pieces that go in different directions
			float degreeChange = 360.0f/splitAmount;
			for (float i = 0; i < 360; i+= degreeChange) 
			{
				float x = Mathf.Cos(i);
				float y = Mathf.Sin(i);

				// Make a copy of this splitter
				GameObject splitBullet = (GameObject)Instantiate(this.gameObject, this.transform.position, Quaternion.identity);

				// Make it go towards the direction we want
				Vector2 vel = new Vector2(x,y);
				// Make sure that bullet goes in the speed we want
				vel.Normalize();
				vel *= base.projectileSpeed;
				// Make the split bullets actual velocity the correct one
				splitBullet.GetComponent<Rigidbody2D>().velocity = vel;
			}
		}
	}
}
