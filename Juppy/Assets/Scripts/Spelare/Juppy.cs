using UnityEngine;
using UnityEngine.SceneManagement;

public class Juppy : MonoBehaviour {

    Rigidbody2D thisRigidbody2D;

    Transform thisTransform;

    Platform deconstructDistance;

    Platform juppy;


    [SerializeField]
    int hearts;

    [SerializeField]
    int jumpForce = 1000000;

    [SerializeField]
    int horizontalMovementSpeed = 1000;

    [SerializeField]
    float sessionHeightScore = 0;

    [SerializeField]
    GameObject heartProjectile;

    public float SessionHeightScore{ get{return sessionHeightScore;} }

    public int Hearts{ get{return hearts;} set{hearts = value;} }

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

	if (Input.GetKeyDown("z")){
	    ShootProjectile();
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

    public void ShootProjectile(){
	if(hearts > 0){
	    Instantiate(heartProjectile, thisTransform.position, thisTransform.rotation);
	   hearts--;
	}
   }

    void OnCollisionEnter2D(Collision2D collision){
        MoodKiller md = collision.collider.GetComponent<MoodKiller>();
        if(md != null){
            foreach(ContactPoint2D point in collision.contacts){
                if(point.normal.y >= 0.9f){
                    md.kill();
                    hearts =+ 5;
                } else {
                    hearts--;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
	if(thisRigidbody2D.velocity.y < 0){

	    if(other.tag == "Platta")
	    {
		// Reset momentum
		thisRigidbody2D.velocity = new Vector2(thisRigidbody2D.velocity.x, 0);

		// Make player jump
		thisRigidbody2D.AddForce(thisTransform.up * jumpForce);
	    }
	}
	else if(other.tag == "MoodKiller"){
	    ReturnToMenu();
	}
    }
    public void ReturnToMenu()
    {
	SceneManager.LoadScene("main");
    }


}
