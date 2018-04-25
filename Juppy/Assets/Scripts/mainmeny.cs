using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmeny : MonoBehaviour {

	// Use this for initialization
	public void PlayGame () {
		SceneManager.LoadScene ("Spelet");
	}

    public void GoBack(){
        SceneManager.LoadScene("main");
    }

	public void QuitGame(){
		Debug.Log ("QUIT");
		Application.Quit ();
	}
}