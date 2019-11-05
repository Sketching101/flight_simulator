using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public static SceneController Instance { get; private set; } // static singleton

    // Use this for initialization
    void Awake () {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameMap");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
