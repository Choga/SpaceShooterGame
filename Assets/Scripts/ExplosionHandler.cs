using UnityEngine;
using System.Collections;

public class ExplosionHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public static void createAndDestroyExplosion(Vector3 position, GameObject explosionAsset)
	{
		GameObject toBeDestroyed = (GameObject) Instantiate (explosionAsset, position, Quaternion.identity);
		Destroy (toBeDestroyed, 1);
	}
}
