using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObject : MonoBehaviour {

    private GameObject objectContainer;
    private GameObject trackerObjectContainer;

	// Use this for initialization
	void Start () {
        objectContainer = GameObject.FindGameObjectWithTag("ObjectContainer");
        trackerObjectContainer = GameObject.FindGameObjectWithTag("TrackerObjectContainer");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
