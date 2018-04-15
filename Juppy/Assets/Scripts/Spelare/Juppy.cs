using UnityEngine;

public class Juppy : MonoBehaviour {

    [SerializeField]
    Rigidbody2D rigidbody2D;

    [SerializeField]
    int jumpForce = 100;

    // Use this for initialization
    void Start () {
	rigidbody2D = this.GetComponent<Rigidbody2D>();
    }
	
    // Update is called once per frame
    //void Update () {
		
    //}
    void OnCollisionEnter2D(Collision2D col) {
	if(col.gameObject.tag == "Platta")
	{
	    rigidbody2D.AddForce(transform.up * JumpForce);
	}
    }
}
