using UnityEngine;

public class Juppy : MonoBehaviour {

    [SerializeField]
    Rigidbody2D rigidbody2D;

    [SerializeField]
    int jumpForce = 400;

    [SerializeField]
    int horizontalMovementSpeed = 7;

    // Use this for initialization
    void Start () {
	rigidbody2D = this.GetComponent<Rigidbody2D>();
    }
	
    void Update() {
        if (Input.GetKey("left")){
	    //rigidbody2D.AddForce(transform.right * -movementForce);
	    rigidbody2D.velocity = new Vector2(-horizontalMovementSpeed, rigidbody2D.velocity.y);
	}
	if (Input.GetKey("right")){
	    //    rigidbody2D.AddForce(transform.right * movementForce);
	    rigidbody2D.velocity = new Vector2(horizontalMovementSpeed, rigidbody2D.velocity.y);

	}
    }

    void OnCollisionEnter2D(Collision2D col) {
	if(col.gameObject.tag == "Platta")
	{
	    rigidbody2D.AddForce(transform.up * jumpForce);
	}
    }
}
