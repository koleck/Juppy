using UnityEngine;
using UnityEngine.SceneManagement;

public class Juppy : MonoBehaviour {

    Rigidbody2D thisRigidbody2D;

    Transform thisTransform;

    Platform deconstructDistance;

    Platform juppy;

    [SerializeField]
    int jumpForce = 1000000;

    [SerializeField]
    int horizontalMovementSpeed = 1000;

    [SerializeField]
    float sessionHeightScore = 0;

    public float SessionHeightScore{ get{return sessionHeightScore;} }
    
    // Use this for initialization
    void Start () {
	thisRigidbody2D = this.GetComponent<Rigidbody2D>();
	thisTransform = this.GetComponent<Transform>();
    }
	
    void Update() {
	if(sessionHeightScore < thisTransform.position.y)
	{
	    sessionHeightScore = thisTransform.position.y;

    }


	if (Input.GetKey("left")){
	    thisRigidbody2D.velocity = new Vector2(-horizontalMovementSpeed, thisRigidbody2D.velocity.y);
	}
	else if (Input.GetKey("right")){
	    thisRigidbody2D.velocity = new Vector2(horizontalMovementSpeed, thisRigidbody2D.velocity.y);
	}
	else{
	    thisRigidbody2D.velocity = new Vector2(0, thisRigidbody2D.velocity.y);
	}
    }

    void OnTriggerEnter2D(Collider2D other) {
	if(thisRigidbody2D.velocity.y < 0){

	if(other.gameObject.tag == "Platta")
	{
	    // Reset momentum
	    thisRigidbody2D.velocity = new Vector2(thisRigidbody2D.velocity.x, 0);

	    // Make player jump
	    thisRigidbody2D.AddForce(thisTransform.up * jumpForce);
	}
	}
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("main");
    }
    public void GameOver()
    {
        if (juppy < Platform.deconstructDistance)
        {
            ReturnToMenu();
        }
    }
}
