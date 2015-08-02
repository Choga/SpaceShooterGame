using UnityEngine;
using System.Collections;

public class LootHandler : MonoBehaviour {

	public static GameObject[] allLootTypes;
	public static string[] allLootNames = {"Engine", "Guns", "Diamond", "Platinum", "PlanetCore"};
	
	private bool addToShip;
	private int shipClass;
	
	public void setClass(int newClass) {
		shipClass = newClass;
	}

	public static GameObject getRandomLoot(int shipClass) {

		//Initializes allLootTypes if not previously initialized
		if (allLootTypes == null) {
			allLootTypes = new GameObject[allLootNames.Length];
			for(int i = 0; i < allLootNames.Length; i++) {
				//Not necessarily the best way to do this, but (right now) get Loot GameObjects by
				//Loading via Resources.load (which searches the Resources folder). Resources folder
				//must be below Assets. Currently: Assets/Prefabs/Loots/Resources. Changing the location
				//of the resources folder will require the argument of this function get changed or a 
				//new means of loading the loot into allLootTypes.
				allLootTypes[i] = (GameObject) Resources.Load (allLootNames[i]);
			}
		}

		// Getting random value for the loot spawn
		int current = (int)(Random.Range (0, shipClass-1));

		// return type of loot, or default back to 4
		if (current < 4) {
			return allLootTypes [current];
		} else {
			return allLootTypes[4];
		}
	}
}
