using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

	void Start() {
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Conversation");

		if (objs.Length > 1) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	void Update()
	{
		
	}
}
