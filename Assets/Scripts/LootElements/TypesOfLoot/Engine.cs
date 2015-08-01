using UnityEngine;
using System.Collections;

public class Engine : Loot {

	private string type = LootHandler.allLootNames[0];

	public override string getType()
	{
		return type;
	}
}
