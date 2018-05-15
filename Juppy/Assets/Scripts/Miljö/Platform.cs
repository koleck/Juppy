using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Platform : MonoBehaviour {

    float deconstructDistance = 1500;

    Transform thisTransform;

    Juppy juppy;

    Juppy ReturnToMenu;

    // Use this for initialization
    void Start () {
	thisTransform = this.GetComponent<Transform>();

	GameObject juppyObjekt = GameObject.FindGameObjectsWithTag("Juppy")[0];
	juppy = juppyObjekt.GetComponent<Juppy>();

    }
	
    // Update is called once per frame
    void Update () {
	if(thisTransform.position.y < juppy.SessionHeightScore - deconstructDistance){
	    Kill();
	}
    }

    public void Kill(){
	if( thisTransform.parent == null){
	    Destroy(this.gameObject);
	}
	else{
	    Destroy(thisTransform.parent.gameObject);
	    Destroy(this.gameObject);
	}
    }
}
