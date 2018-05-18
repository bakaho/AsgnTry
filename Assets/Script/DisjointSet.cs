//Gao Ya
//54380279
//for further usage
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisjointSet : MonoBehaviour {
	public int allNum;
	private int[] name;

	DisjointSet(){
		for (int i = 0; i < allNum; i++) {
			name[i] = i;
		}
	}


	int Find(int i) {
		return name[i];
	}

	void Union(int i, int j){
		if (Find(i) != Find(j)) {
			int temp = name[j];
			for (int k = 0; k < 25; k++) {
				if (name[k] == temp)
					name[k] = name[i];
			}
		}
	}






}
