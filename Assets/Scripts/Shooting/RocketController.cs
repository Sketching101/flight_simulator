using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

    public Vector3 TargetPosition;

    public Rigidbody rb;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if(TargetPosition == Vector3.zero)
        {
            return;
        }

        transform.LookAt(TargetPosition);
        rb.velocity = 100 * transform.forward;
	}
}
