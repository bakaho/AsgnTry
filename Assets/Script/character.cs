//Gao Ya
//54380279
//old version
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour {
//
//	private bool finish = true;
//	private Vector3 pos;
//	public GameObject biglines;
//	public static bool bigIsOn = false;
//
//
//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		//bigIsOn = false;
//		if(Input.GetMouseButtonDown(0))
//		{
//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//
//			RaycastHit hit = new RaycastHit();
//
//			if (Physics.Raycast(ray, out hit))
//
//			{
//				if (hit.collider.name == "Land")
//
//				{
//					pos = hit.point;
//
//					finish = false;
//				}
//			}
//		}
//
//		if(!finish)
//		{
//			Vector3 offset = pos - transform.position;
//
//			transform.position += offset.normalized * 20 * Time.deltaTime;
//
//			if(Vector3.Distance(pos, transform.position)<1f)
//
//			{
//				transform.position = pos;
//
//				finish = true;
//			}
//		}
//
//	}
//
//
//	void OnTriggerEnter(Collider other)
//	{
//		//if it eat the pickup object, make it inActive
//		if (other.gameObject.CompareTag ("bigLine")) {
//			bigIsOn = true;
//		}
//	}
//
//	void OnTriggerStay(Collider other)
//	{
//		//if it eat the pickup object, make it inActive
//		if (other.gameObject.CompareTag ("bigLine")) {
//			bigIsOn = true;
//		}
//	}
//
//	void OnTriggerExit(Collider other)
//	{
//		//if it eat the pickup object, make it inActive
//		if (other.gameObject.CompareTag ("bigLine")) {
//			bigIsOn = false;
//		}
//	}
}
