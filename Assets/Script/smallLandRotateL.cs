//Gao Ya
//54380279
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallLandRotateL : MonoBehaviour {
	//select the part
	public GameObject smallland;
	private int turnRight = 0;
	public static bool up = false;
	private int turnLeft = 0;


	Renderer rend;
	public Material[] skin;

	void Start () {
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		rend.sharedMaterial = skin [0];

	}

	void OnMouseDrag(){
		if (Input.GetAxis ("Mouse Y") > 0.1) {
			if (turnRight <= 0) {
				turnRight = 180;
				//change the upper side information
				up = !up;
			}
		}
		if (Input.GetAxis ("Mouse Y") < -0.1) {
			if (turnLeft <= 0) {
				turnLeft = 180;
				//change the upper side information
				up = !up;
			}
		}
	}

	void OnMouseEnter(){
		rend.sharedMaterial = skin [1];
	}
	void OnMouseExit(){
		rend.sharedMaterial = skin [0];
	}

	void Update () {
		//auto rotation (different axis)
		if (turnRight > 0) {
			turnRight -= 6; 
			if(up)
				smallland.transform.Rotate (Vector3.right, 6f);
			else
				smallland.transform.Rotate (Vector3.right, -6f);
		} 
		if (turnLeft > 0) {
			turnLeft -= 6; 
			if(up)
				smallland.transform.Rotate (Vector3.right, -6f);
			else
				smallland.transform.Rotate (Vector3.right, 6f);
		} 

	}

}
