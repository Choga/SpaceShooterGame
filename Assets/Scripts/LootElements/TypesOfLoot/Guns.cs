using UnityEngine;
using System.Collections;

public class Guns : Loot
{
	private string type = "Guns";

	public override string getType()
	{
		return type;
	}
}
