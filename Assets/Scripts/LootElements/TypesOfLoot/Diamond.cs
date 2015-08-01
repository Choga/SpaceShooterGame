using UnityEngine;
using System.Collections;

public class Diamond : Loot {

	private string type = LootHandler.allLootNames[2];
	public override string getType(){
		return type;
	}
}
