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
    int moodkiller;

    [SerializeField]
    int jumpForce = 1000000;

    [SerializeField]
    int horizontalMovementSpeed = 1000;

    [SerializeField]
    float sessionHeightScore = 0;

    [SerializeField]
    GameObject heartProjectile;

    [SerializeField]
    float sessionHighestXCoordinate = 0;

    int moodkillersDefeated = 0;

    Gyroscope gyro;


    public float MoodKillersDefeated{ get{return moodkillersDefeated;} }

    public float SessionHeightScore{ get{return sessionHeightScore;}set{sessionHeightScore = value;} }

    public float SessionHighestXCoordinate{ get{return sessionHighestXCoordinate;} }

    public int Hearts{ get{return hearts;} set{hearts = value;} }

    public int MoodKiller { get { return moodkiller; } set { moodkiller = value; } }

    // Use this for initialization
    void Start () {
	thisRigidbody2D = this.GetComponent<Rigidbody2D>();
	thisTransform = this.GetComponent<Transform>();

	if (SystemInfo.supportsGyroscope)
	{
	    gyro = Input.gyro;
	    gyro.enabled = true;
	}
    }
	
    void Update() {
	if(sessionHeightScore < thisTransform.position.y)
	{
	    sessionHeightScore = thisTransform.position.y;
	}
	else if(thisTransform.position.y < sessionHeightScore - 2000)
	{
	    Kill();
	}

	if(sessionHighestXCoordinate < thisTransform.position.x)
	{
	    sessionHighestXCoordinate = thisTransform.position.x;
	}

	if (Input.GetKeyDown("z")){
	    ShootProjectile();
	}

	if (gyro != null)
	{
	    thisRigidbody2D.velocity = new Vector2(-Input.gyro.rotationRateUnbiased.y, thisRigidbody2D.velocity.y);
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
	if(other.tag == "MoodKiller"){
	    if(hearts < 1){
		ReturnToMenu();
	    }
	    else{
		hearts--;
	    }

	    other.GetComponent<MoodKiller>().kill();
	    moodkillersDefeated ++;
	}

	if(other.tag == "HeadHitbox"){
	    other.gameObject.transform.parent.GetComponent<MoodKiller>().kill();
	    moodkillersDefeated ++;
	    
            // hoppa
	    thisRigidbody2D.velocity = new Vector2(thisRigidbody2D.velocity.x, 0);
	    thisRigidbody2D.AddForce(thisTransform.up * jumpForce);
	}

	if(other.tag == "Heart"){
	    Heart heart = other.GetComponent<Heart>();
	    heart.Kill();
	    hearts++;
	}

    }

    public void Kill(){
	UpdateHighscores();
	ReturnToMenu();

    }

    void UpdateHighscores(){
	if(hearts > Highscores.HighestHeartCount)
	{
		Highscores.HighestHeartCount = hearts;
	}
	    	if(sessionHeightScore > Highscores.HighestHeight)
	{
		Highscores.HighestHeight = sessionHeightScore;
	}
	    	if(moodkillersDefeated > Highscores.HighestMoodkillerCount)
	{
		Highscores.HighestMoodkillerCount = moodkillersDefeated;
	}
    }

    public void ReturnToMenu()
    {
	SceneManager.LoadScene("main");
    }


}
