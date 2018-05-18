//Gao Ya
//54380279
//old version
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clicktest : MonoBehaviour {
	public GameObject player;
	void OnMouseClick()
	{
		player.gameObject.transform.position = this.transform.position;
		print ("~~~");
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
