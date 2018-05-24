using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    Juppy juppy;
    Transform juppyTransform;

    Transform thisTransform;

    float shakeAmount = 20;

    public float ShakeTime{get;set;}

    [SerializeField]
    private bool scrollVertically;

    public bool ScrollVertically { get{return scrollVertically;} set{scrollVertically = value;} }


    void Start () {
	GameObject juppyObjekt = GameObject.FindGameObjectsWithTag("Juppy")[0];
	juppy = juppyObjekt.GetComponent<Juppy>();
	juppyTransform = juppy.GetComponent<Transform>();

	thisTransform = this.GetComponent<Transform>();
    }


    void LateUpdate() {
	if(ShakeTime > 0){

	    ShakeScreen();

	    ShakeTime -= Time.deltaTime;
	}
	else{
	    thisTransform.position = new Vector3(juppyTransform.position.x, juppyTransform.position.y, thisTransform.position.z);
	}
    }

    private void ShakeScreen(){
	Debug.Log(Random.value);
	thisTransform.position = new Vector3(juppyTransform.position.x + Random.value * shakeAmount, juppyTransform.position.y + Random.value * shakeAmount, -10);
    }
}
