using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAxisPlatform : MonoBehaviour {

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
	if(thisTransform.position.x < juppy.SessionHighestXCoordinate - deconstructDistance){
	    Destroy(this.gameObject);
	}
    }

}
