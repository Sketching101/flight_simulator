using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_spheres : MonoBehaviour {

    public bool visible = true;
    public float invis_timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!visible)
        {
           invis_timer += Time.deltaTime;
            if (invis_timer >= 5.0f) {
                visible = true;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLISION");
        if (other.gameObject.tag == "bullet" && visible) { 
              Destroy(other.gameObject);
              visible = false;
              gameObject.GetComponent<MeshRenderer>().enabled = false;
              invis_timer = 0.0f;
        }
    }
}
