//Gao Ya
//54380279
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landFace : MonoBehaviour {
	//select the part that we want to operate
	public GameObject land;
	private int turnRight = 0;
	private int turnLeft = 0;

	Renderer rend;
	public Material[] skin;

	void Start () {
		//make sure the renderer is on
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		//default skin
		rend.sharedMaterial = skin [0];

	}

	void OnMouseDrag(){
		//measure the mouse movement
		if (Input.GetAxis ("Mouse X") > 0.5) {
			if (turnRight <= 0) {
				turnRight = 90;
			}				
		}
		if (Input.GetAxis ("Mouse X") < -0.5) {
			if (turnLeft <= 0) {
				turnLeft = 90;
			}				
		}
	}

	//mouse over effect
	void OnMouseEnter(){
		rend.sharedMaterial = skin [1];
	}
	void OnMouseExit(){
		rend.sharedMaterial = skin [0];
	}

	void Update () {
		//auto rotation
		if (turnRight > 0) {
			turnRight -= 3; 
			land.transform.Rotate (Vector3.up, -3f);
		} 
		if (turnLeft > 0) {
			turnLeft -= 3; 
			land.transform.Rotate (Vector3.up, 3f);
		} 

	}

}
