using UnityEngine;
using System.Collections;

public class LootHandler : MonoBehaviour {

	public GameObject[] allLootTypes;
	public static string[] allLootNames = {"Engine", "Guns", "Diamond", "Platinum", "PlanetCore"};
	
	private bool addToShip;
	private int shipClass;

	public void setClass(int newClass) {
		shipClass = newClass;
	}

	public GameObject getRandomLoot(int shipClass) {

		// Getting random value for the loot spawn
		int current = (int)(Random.Range (0, shipClass-1));
		switch (current) {
		case 0:
			return allLootTypes[0];
		case 1:
			return allLootTypes[1];
		case 2:
			return allLootTypes[2];
		case 3:
			return allLootTypes[3];
		default:
			return allLootTypes[4];
		}
	}
}
