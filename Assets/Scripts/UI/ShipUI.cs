using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShipControllerNS;
using UnityEngine.UI;

public class ShipUI : MonoBehaviour
{
    public ShipController shipController;

    public Text VelocityDisplay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        VelocityDisplay.text = string.Format("{0} m/s", shipController.velocity_display);
	}
}
