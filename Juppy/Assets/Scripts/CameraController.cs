using UnityEngine;

public class CameraController : MonoBehaviour {

    Juppy juppy;
    Transform juppyTransform;

    Transform thisTransform;

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
	thisTransform.position = new Vector3(juppyTransform.position.x, juppyTransform.position.y, thisTransform.position.z);
    }
}
