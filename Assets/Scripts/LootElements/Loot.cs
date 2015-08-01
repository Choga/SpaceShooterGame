using UnityEngine;
using System.Collections;

public abstract class Loot : MonoBehaviour
{
	public abstract string getType();

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player") {
			PlayerMovement player = (PlayerMovement) collider.gameObject.GetComponent ("PlayerMovement");
			if(!player.isFull()) {
				player.addLoot (getType());
				Destroy (this.gameObject);
			}
		}
	}
}
