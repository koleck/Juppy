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
    public static AudioClip heartLjud;

    public static AudioClip lostLife;
    public static AudioClip dies;
    private AudioSource src;
    private AudioSource src2;

    private AudioSource src4;
    private AudioSource src5;


    // Used by on-screen buttons
    [SerializeField]
    public bool uiMoveLeft = false;
    [SerializeField]
    public bool uiMoveRight = false;

    public bool UIMoveLeft { get { return uiMoveLeft; } set { uiMoveLeft = value; } }

    public bool UIMoveRight { get { return uiMoveRight; } set { uiMoveRight = value; } }

    public bool dead = false;

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


    SpriteRenderer backgroundSpriteRenderer;

    SpriteRenderer thisSpriteRenderer;

    int moodkillersDefeated = 0;

    CameraController camera;

    public float MoodKillersDefeated { get { return moodkillersDefeated; } }

    public float SessionHeightScore { get { return sessionHeightScore; } set { sessionHeightScore = value; } }

    public int Hearts { get { return hearts; } set { hearts = value; } }

    public int MoodKiller { get { return moodkiller; } set { moodkiller = value; } }

    public int höjd = 2000;
    // Use this for initialization
    void Start()
    {
        
        src = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        src2 = GameObject.Find("heartSound").GetComponent<AudioSource>();

        src4 = GameObject.Find("loselife").GetComponent<AudioSource>();
        src5 = GameObject.Find("juppydies").GetComponent<AudioSource>();
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

      
        if (dead == true)
        {
            juppyDieSound();
        }


	if (sessionHeightScore < thisTransform.position.y)
	{
	    sessionHeightScore = thisTransform.position.y;

        } 

	else if (thisTransform.position.y < sessionHeightScore - höjd)
	{
            dead = true;
	    Kill();

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

  
   

    void lostLifeSound()
    {
        src4.PlayOneShot(lostLife, 0.2f);
        src4.Play();
    }

    void jumpSoundet(){
        src.PlayOneShot(jumpSound, 0.1f);
        src.Play();

    }

    void collectHeartSound(){
        src2.PlayOneShot(heartLjud, 0.2f);
        src2.Play();
    }

    void juppyDieSound()
    {
        src5.PlayOneShot(dies, 0.2f);
        src5.Play();
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
        jumpSoundet();
    }

    void miniJump()
    {
        thisTransform.localScale = new Vector3(0.3f, 0.18f, 1);


        // Reset momentum
        thisRigidbody2D.velocity = new Vector2(thisRigidbody2D.velocity.x, 0);

        // Make player jump
        thisRigidbody2D.AddForce(thisTransform.up * 500000);

        jumpSoundet();
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

                miniJump();



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
                lostLifeSound();

		ShakeScreen();

		
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
                lostLifeSound();

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
            collectHeartSound();
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
