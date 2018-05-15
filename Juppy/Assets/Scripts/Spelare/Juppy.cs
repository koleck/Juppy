using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Juppy : MonoBehaviour
{

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
        camera = GameObject.Find("Main Camera").GetComponent<CameraController>();

	thisRigidbody2D = this.GetComponent<Rigidbody2D>();
        thisTransform = this.GetComponent<Transform>();

        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
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

        if (-Input.acceleration.z != 0)
        {
            float gyroZ = -Input.acceleration.z;

            if (gyroZ > horizontalMovementSpeed)
            {
                gyroZ = horizontalMovementSpeed;
            }
            else if (gyroZ < -horizontalMovementSpeed)
            {
                gyroZ = -horizontalMovementSpeed;
            }

            thisRigidbody2D.velocity = new Vector2(gyroZ, thisRigidbody2D.velocity.y);
        }


        if (Input.GetKey("left"))
        {
            thisRigidbody2D.velocity = new Vector2(-horizontalMovementSpeed, thisRigidbody2D.velocity.y);
        }
        else if (Input.GetKey("right"))
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

        // Reset momentum
        thisRigidbody2D.velocity = new Vector2(thisRigidbody2D.velocity.x, 0);

        // Make player jump
        thisRigidbody2D.AddForce(thisTransform.up * jumpForce);

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

                other.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }

            if (other.tag == "FallingPlatform")
            {
                Jump();

                Transform platformParent = other.transform.parent;

                MakePlatformSolid(platformParent);

                other.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }

        }
        if (other.tag == "MoodKiller")
        {

            if (hearts < 1)
            {
                ReturnToMenu();
            }
            else
            {
                hearts--;

                Jump();

                other.GetComponent<SpriteRenderer>().color = Color.red;
            }

            other.GetComponent<MoodKiller>().kill();
            moodkillersDefeated++;
        }

        if (other.tag == "HeadHitbox")
        {

            Transform moodkillerTransform = other.gameObject.transform.parent;

            moodkillerTransform.GetComponent<SpriteRenderer>().color = Color.red;

            moodkillerTransform.GetComponent<MoodKiller>().kill();

            moodkillersDefeated++;

            Jump();
        }

        if (other.tag == "Heart")
        {
            Heart heart = other.GetComponent<Heart>();
            heart.Kill();
            hearts++;

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

    void UpdateHighscores()
    {
        if (hearts > Highscores.HighestHeartCount)
        {
            Highscores.HighestHeartCount = hearts;
        }
        if (sessionHeightScore > Highscores.HighestHeight)
        {
            Highscores.HighestHeight = (int)sessionHeightScore;
        }
        if (moodkillersDefeated > Highscores.HighestMoodkillerCount)
        {
            Highscores.HighestMoodkillerCount = moodkillersDefeated;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("main");
    }
}
