//Gao Ya
//54380279

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class characterX : MonoBehaviour {

	public static characterX instance;
	
	//the respawn point
	public GameObject reborn;

	//the animator
	static Animator anim;
	//NavMeshAgent nav;

	//the destination 
	private Transform moveHolder;

	void Awake() {
		instance = this;
		anim = GetComponent<Animator> ();
		moveHolder = new GameObject("MoveHolder").transform;
		moveHolder.SetParent(this.transform.parent);
		moveHolder.position = this.transform.position;
	}
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.name == "Land")
				{
					moveHolder.position = hit.point;
					//turn to the mouse direction
					transform.LookAt (moveHolder.position);
					//nav.SetDestination (pos);
				}
			}
		}

		//move
		Vector3 offset = moveHolder.position - transform.position;
		transform.position += offset.normalized * 25 * Time.deltaTime;
		if(Vector3.Distance(moveHolder.position, transform.position)<1f) {
			transform.position = moveHolder.position;
			anim.SetBool ("IsRunning", false);
		}
		else {
			anim.SetBool ("IsRunning", true);
		}

		//if die
		if (anim.GetBool ("IsDead")) {
			//press any key to restart
			if (Input.anyKey) {				
				transform.position = reborn.transform.position;
				moveHolder.position = this.transform.position;
				this.ResetState();
			}
		}
	}

	public void ResetState() {
		anim.SetBool ("IsDead", false);
		anim.SetBool ("IsWin", false);
	}
	public void UpdateState(bool win) {
		this.ResetState();
		moveHolder.position = transform.position;
		if (win) {
			anim.SetBool ("IsWin", true);
		}
		else {
			anim.SetBool ("IsDead", true);
		}
	}
}
