using UnityEngine;
using System.Collections;

public class HubWorld : MonoBehaviour {

	public GameObject storeUI;
	private bool storeSpawned = false;

	private int loot1;
	private int loot2;
	private int loot3;
	private int loot4;
	private int loot5;

	// Use this for initialization
	void Start () 
	{
		// Make UI inactive to start
		storeUI.SetActive (false);
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		// If the player collides with the hub world then we spawn the GUI
		if (collision.gameObject.tag == "Player") {
			spawnGUI();
		}
	}

	// Creates the GUI
	public void spawnGUI()
	{
		// We keep track of if the store is spawned or not
		storeSpawned = true;
		// This makes the GUI visible
		storeUI.SetActive (true);
		// pause the game
		pause ();
	}

	public void unSpawnGUI()
	{
		// Makes GUI invisible
		storeUI.SetActive (false);
		// Makes sure we know store is no longer spawned
		storeSpawned = false;
		// unPause the game
		unPause ();
	}

	// Pauses everything
	public void unPause()
	{
		Time.timeScale = 1;
	}

	// Unpauses everything
	public void pause()
	{
		Time.timeScale = 0;
	}
}
