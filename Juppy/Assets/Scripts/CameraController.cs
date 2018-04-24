using UnityEngine;

public class CameraController : MonoBehaviour {

    float cameraPosition = 0;

    Juppy juppy;

    Transform thisTransform;


    void Start () {
	GameObject juppyObjekt = GameObject.FindGameObjectsWithTag("Juppy")[0];
	juppy = juppyObjekt.GetComponent<Juppy>();

	thisTransform = this.GetComponent<Transform>();
    }


    void LateUpdate() {
	if(juppy.SessionHeightScore > cameraPosition){
	    thisTransform.position = new Vector3(thisTransform.position.x, juppy.SessionHeightScore, thisTransform.position.z);
	}
    }

}
