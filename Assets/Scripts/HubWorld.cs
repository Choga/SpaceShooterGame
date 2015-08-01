using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HubWorld : MonoBehaviour {

	public GameObject storeUI;
	private bool storeSpawned = false;

	private int engine;
	private int guns;
	private int diamond;
	private int platinum;
	private int planetCore;

	Dictionary<string, int> bank;

	// Use this for initialization
	void Start () 
	{
		// Make UI inactive to start
		storeUI.SetActive (false);
		// Creating dictionary to store all loot types and amounts
		bank = new Dictionary<string, int> ();
		// Making sure each one starts at 0 so we don't have to worry about it later
		foreach (string lootType in LootHandler.allLootNames) 
		{
			bank.Add(lootType, 0);
		}
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		// If the player collides with the hub world then we spawn the GUI
		if (collision.gameObject.tag == "Player") {
			PlayerMovement player = collision.GetComponent<PlayerMovement>();

			// calls function to get player loot into bank
			storePlayerLoot(player);

			spawnGUI();
		}
	}
	// Takes all of a player's loot and puts it into the bank, does not clear the player's inventory
	private void storePlayerLoot(PlayerMovement player)
	{
		// Get all the players loot
		string[] playerManifest = player.bankManifest();
		// Loop through all of the players loot and put it in the bank
		foreach(string lootItem in playerManifest)
		{// Make sure that the loot listed is not null
			if(lootItem != null)
			{
				int currVal = 0;
				if(bank.TryGetValue(lootItem, out currVal))
				{
					bank[lootItem] = currVal + 1;
				}// This else statement is in here because I know we will mess up on adding loot (forget to put it in lootname list
				// or game object list)
				else
				{
					// If the item was not in the bank from initialization already, something went very wrong
					Debug.Log("Item " + lootItem + " was not in the set of known loot");
				}
			}
		}
		
		foreach(string lootItem in bank.Keys)
		{
			Debug.Log(lootItem + " : " + bank[lootItem]);
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
