using UnityEngine;
using System.Collections;

public abstract class Loot : MonoBehaviour
{
	// Every loot has a type to it that identifies it
	public abstract string getType();

	void OnTriggerEnter2D(Collider2D collider)
	{
		// checking if we collided with player
		if (collider.gameObject.tag == "Player") {
			// Gettin player to give it loot
			PlayerMovement player = (PlayerMovement) collider.gameObject.GetComponent ("PlayerMovement");
			// If player could hold the loot, we destroy the object
			if(player.addLoot (getType()))
			{
				Destroy (this.gameObject);
			}
		}
	}
}
