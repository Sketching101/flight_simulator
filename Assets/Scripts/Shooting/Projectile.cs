using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public GameObject projectile;
    public Transform Spawnpoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject clone = Instantiate(projectile, Spawnpoint.position, Quaternion.identity) as GameObject;


            clone.GetComponent<Rigidbody>().velocity = Spawnpoint.TransformDirection(Vector3.forward*20);
        }
    }
}
