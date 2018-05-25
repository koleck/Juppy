using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Juppy : MonoBehaviour
{

    Rigidbody2D thisRigidbody2D;

    Transform thisTransform;

    Platform deconstructDistance;

    Platform juppy;

    public static AudioClip jumpSound;
    private AudioSource src;


    // Used by on-screen buttons
    [SerializeField]
    public bool uiMoveLeft = false;
    [SerializeField]
    public bool uiMoveRight = false;

    public bool UIMoveLeft { get { return uiMoveLeft; } set { uiMoveLeft = value; } }

    public bool UIMoveRight { get { return uiMoveRight; } set { uiMoveRight = value; } }

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

    SpriteRenderer backgroundSpriteRenderer;

    SpriteRenderer thisSpriteRenderer;

    int moodkillersDefeated = 0;

    CameraController camera;

    public float MoodKillersDefeated { get { return moodkillersDefeated; } }

    public float SessionHeightScore { get { return sessionHeightScore; } set { sessionHeightScore = value; } }

    public int Hearts { get { return hearts; } set { hearts = value; } }

    public int MoodKiller { get { return moodkiller; } set { moodkiller = value; } }


    // Use this for initialization
    void Start()
    {
        
        //src = GetComponent<AudioSource>();
        camera = GameObject.Find("Main Camera").GetComponent<CameraController>();


        thisRigidbody2D = this.GetComponent<Rigidbody2D>();
        thisTransform = this.GetComponent<Transform>();

        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
	if(thisTransform.localScale.y < 0.3){
	    thisTransform.localScale += new Vector3(0, 0.02f, 0);

	}
	else if (thisTransform.localScale.y > 0.3){
	    thisTransform.localScale = new Vector3(0.3f, 0.3f, 1);
	}


	if (sessionHeightScore < thisTransform.position.y)
	{
	    sessionHeightScore = thisTransform.position.y;
	}
	else if (thisTransform.position.y < sessionHeightScore - 2000)
	{
	    Kill();
	}


        if (Input.GetKeyDown("z"))
        {
            ShootProjectile();
        }

        if (Input.GetKey("left") || uiMoveLeft)
        {
            thisRigidbody2D.velocity = new Vector2(-horizontalMovementSpeed, thisRigidbody2D.velocity.y);
        }
        else if (Input.GetKey("right") || uiMoveRight)
        {
            thisRigidbody2D.velocity = new Vector2(horizontalMovementSpeed, thisRigidbody2D.velocity.y);
        }
        else
        {
            thisRigidbody2D.velocity = new Vector2(0, thisRigidbody2D.velocity.y);
        }
    }

    public void ShootProjectile()
    {
        if (hearts > 0)
        {
            Instantiate(heartProjectile, thisTransform.position, thisTransform.rotation);
            hearts--;
        }
    }

    void Jump()
    {
	// Shrink
	thisTransform.localScale = new Vector3(0.3f, 0.18f, 1);


	// Reset momentum
        thisRigidbody2D.velocity = new Vector2(thisRigidbody2D.velocity.x, 0);

        // Make player jump
        thisRigidbody2D.AddForce(thisTransform.up * jumpForce);

        //Play jump sound

       // src.Play();

    }

    void ShakeScreen()
    {
	camera.ShakeTime += 0.2f;

    }

    void MakePlatformSolid(Transform platform)
    {
        platform.GetComponent<BoxCollider2D>().isTrigger = false;

        platform.GetComponent<Rigidbody2D>().gravityScale = 100;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (thisRigidbody2D.velocity.y < 0)
        {

            if (other.tag == "Platform")
            {
                Jump();

                other.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0.25f, 0.25f, 0.25f, 0.8f, 0.8f, 0.8f);

            }

            if (other.tag == "FallingPlatform")
            {
                Jump();
                Jump();
                Transform platformParent = other.transform.parent;

                MakePlatformSolid(platformParent);

                other.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

            }

        }

        if (other.tag == "HeadHitbox")
        {

            Transform moodkillerTransform = other.gameObject.transform.parent;

            moodkillerTransform.GetComponent<SpriteRenderer>().color = Color.red;

            moodkillerTransform.GetComponent<MoodKiller>().kill();

            moodkillersDefeated++;

            Jump();

	    ShakeScreen();
        }
        else if (other.tag == "AngryMoodKillerHeadHitbox")
        {
            Transform moodkillerTransform = other.gameObject.transform.parent;

            moodkillerTransform.GetComponent<SpriteRenderer>().color = Color.red;

            moodkillerTransform.GetComponent<AngryMoodKiller>().kill();

            moodkillersDefeated++;

            Jump();

	    ShakeScreen();
        }
        else if (other.tag == "MoodKiller")
        {

            if (hearts < 1)
            {
                Kill();
            }
            else
            {
                StartCoroutine(BecomeSad(1.0f));
                hearts--;

		ShakeScreen();

		Jump();
            }

            other.GetComponent<MoodKiller>().kill();
            moodkillersDefeated++;
        }
	else if (other.tag == "AngryMoodKiller" ){

            if (hearts < 1)
            {
                Kill();
            }
            else
            {
                StartCoroutine(BecomeSad(1.0f));
                hearts--;

		ShakeScreen();

		Jump();
            }

            other.GetComponent<AngryMoodKiller>().kill();
            moodkillersDefeated++;

	}

	else if (other.tag == "Heart")
	{
	    Heart heart = other.GetComponent<Heart>();
	    heart.Kill();
	    hearts++;

	    ShakeScreen();

	    StartCoroutine(BecomeHappier(1.0f));
	}

    }

    public void Kill()
    {
        UpdateHighscores();
        ReturnToMenu();

    }

    private IEnumerator BecomeHappier(float time)
    {
        thisSpriteRenderer.color = Color.green;

        yield return new WaitForSeconds(time);

        thisSpriteRenderer.color = Color.white;

    }

    private IEnumerator BecomeSad(float time)
    {
        thisSpriteRenderer.color = Color.red;

        yield return new WaitForSeconds(time);

        thisSpriteRenderer.color = Color.white;

    }

    void UpdateHighscores()
    {
        int highscoreheight = 0;
        int highscorehearts = 0;
        int highscoremoodkiller = 0;

        if (hearts > PlayerPrefs.GetInt("hearts", highscorehearts))
        {
            //Highscores.HighestHeartCount = hearts;
            PlayerPrefs.SetInt("hearts", hearts);
        }
        if (sessionHeightScore > PlayerPrefs.GetInt("height", highscoreheight) )
        {
            //Highscores.HighestHeight = (int)sessionHeightScore;

            PlayerPrefs.SetInt("height", (int)sessionHeightScore);
        }
        if (moodkillersDefeated > PlayerPrefs.GetInt("moodkiller", highscoremoodkiller))
        {
            //Highscores.HighestMoodkillerCount = moodkillersDefeated;
            PlayerPrefs.SetInt("moodkiller", moodkillersDefeated);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("main");
    }
}
