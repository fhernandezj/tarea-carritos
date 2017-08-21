using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llanta : MonoBehaviour {
    public WheelCollider collider;
	// Use this for initialization
	void Start () {
        collider = GetComponentInParent<WheelCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
