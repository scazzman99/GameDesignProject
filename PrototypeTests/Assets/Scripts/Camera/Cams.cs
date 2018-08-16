using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cams : MonoBehaviour {

    private Vector3 offset;
    public Transform target;
	// Use this for initialization
	void Start () {
        offset = transform.position - target.position; // keeps cam at same distance
   
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.position + offset;
	}
}
