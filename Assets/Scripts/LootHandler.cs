using UnityEngine;
using System.Collections;

public class LootHandler : MonoBehaviour {

	private string value;
	private bool addToShip;
	private int shipClass;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setClass(int newClass) {
		shipClass = newClass;
	}

	public void setValue() {

		int current = (int)(Random.Range (0, shipClass-1));
		switch(current) {
			case 0:
			value = "Used Tissue";
			break;
			case 1:
			value = "A Little Child's Candy";
				break;
			case 2:
			value = "Pocket Change";
				break;
			case 3:
			value = "Pew Pew Machine";
				break;
			case 4:
			value = "Hookers";
				break;
			default:
			value = "Used Condoms";
				break;
		}
	}

	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			PlayerMovement player = (PlayerMovement) collider.gameObject.GetComponent ("PlayerMovement");
			if(!player.isFull()) {
				setValue ();
				player.addLoot (value);
				Destroy (this.gameObject);
			}
		}
	}
}
