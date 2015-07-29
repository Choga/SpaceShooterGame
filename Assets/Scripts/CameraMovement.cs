using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private GameObject player; 
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.position = player.transform.position - new Vector3(0 , 0, 10);
	}
}
