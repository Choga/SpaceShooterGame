using UnityEngine;
using System.Collections;

public class Platinum : Loot
{
	private string type = LootHandler.allLootNames[3];
	public override string getType()
	{
		return type;
	}
}
