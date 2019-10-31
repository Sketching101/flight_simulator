using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosInit : MonoBehaviour {

    public Transform PlayerPos;
    public Transform CameraPos;
    public Transform TranslateToPos;

	// Use this for initialization
	void Start () {
        PlayerPos.position = TranslateToPos.position;// - CameraPos.position;
	}
}
