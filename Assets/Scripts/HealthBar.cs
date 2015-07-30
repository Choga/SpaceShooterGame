using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour 
{
	private float origSize;
	 
	// Use this for initialization
	void Start () 
	{
		origSize = this.transform.localScale.x;

	}

	// Changes health to the given amount
	public void setHealth(float health)
	{
		Vector3 origScale = this.transform.localScale;
		this.transform.localScale = new Vector3(origSize * health / 100, origScale.y, origScale.z);
	}
}
