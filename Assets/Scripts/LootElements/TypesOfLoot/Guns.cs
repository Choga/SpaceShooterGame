using UnityEngine;
using System.Collections;

public class Guns : Loot
{
	private string type = LootHandler.allLootNames[1];

	public override string getType()
	{
		return type;
	}
}
