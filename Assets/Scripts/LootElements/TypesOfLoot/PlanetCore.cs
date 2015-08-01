using UnityEngine;
using System.Collections;

public class PlanetCore :Loot 
{
	private string type = LootHandler.allLootNames[4];
	public override string getType()
	{
		return type;
	}
}
