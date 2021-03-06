using System.Collections;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    [SerializeField]
    float lastGeneratedObjectX = 0;

    [SerializeField]
    float lastGeneratedObjectY = -500;

    [SerializeField]
    int platformMaxHeightDelta = 170;

    [SerializeField]
    int platformMaxWidthDelta = 750;

    [SerializeField]
    float startGenerationAt = 1500;

    [SerializeField]
    GameObject[] platforms = new GameObject[2];

    [SerializeField]
    GameObject[] dangerousPlatforms = new GameObject[2];

    [SerializeField]
    GameObject heart;

    [SerializeField]
    GameObject moodKiller;

    [SerializeField]
    GameObject angryMoodKiller;

    [SerializeField]
    GameObject livingMoodKiller;

    bool livingMoodKillerCreated = false;

    [SerializeField]
    bool generateHorizontally = false;

    Juppy juppy;

    Transform juppyTransform;


    System.Random rnd = new System.Random();

    [SerializeField]
    int changePlatformGenerationAxisTime = 1000;

    [SerializeField]
    int changePlatformGenerationAxisTimer = 0;


    void Start()
    {
	GameObject juppyObjekt = GameObject.FindGameObjectsWithTag("Juppy")[0];
	juppy = juppyObjekt.GetComponent<Juppy>();
	juppyTransform = juppy.GetComponent<Transform>();
    }

    GameObject GetRandomPlatform()
    {
	int platformOrFloatingPlatform = 10;
	if(juppy.SessionHeightScore > 10000 && juppy.SessionHeightScore < 20000) {
	    platformOrFloatingPlatform = rnd.Next(5, 15);
	}
	else if(juppy.SessionHeightScore > 20000 && juppy.SessionHeightScore < 30000){
	    platformOrFloatingPlatform = rnd.Next(0, 12);
	}
	else if(juppy.SessionHeightScore > 30000){
	    // Only return most dangerous platform
	    return dangerousPlatforms[rnd.Next(0)];
	}

	if(platformOrFloatingPlatform >= 10) {

	    return platforms[rnd.Next(0, platforms.Length)];
	}
	else{
	    return dangerousPlatforms[rnd.Next(0, platforms.Length)];
	}
    }

    GameObject GetMoodKiller(){
	if(juppy.SessionHeightScore > 20000) {
	    return angryMoodKiller;
	}
	else{
	    return moodKiller;
	}
    }



    void AttemptGeneratePlatformHorizontally()
    {
	// If last generated platform is left of right side of screen
	if (lastGeneratedObjectX + platformMaxWidthDelta < juppyTransform.position.x + startGenerationAt)
	{
	    float newPlatformX = rnd.Next((int)lastGeneratedObjectX + platformMaxWidthDelta - 400,(int)lastGeneratedObjectX + platformMaxWidthDelta);

	    float newPlatformY;

	    newPlatformY = rnd.Next((int)lastGeneratedObjectY - platformMaxHeightDelta ,(int)lastGeneratedObjectY + platformMaxHeightDelta);

	    lastGeneratedObjectX = newPlatformX;
	    lastGeneratedObjectY = newPlatformY;

	    GameObject platformToCreate = GetRandomPlatform();

	    Instantiate(platformToCreate, new Vector2(newPlatformX, newPlatformY), platformToCreate.transform.rotation);

	    AttemptGenerateHeart(lastGeneratedObjectX, lastGeneratedObjectY);

	}
    }


    void AttemptGeneratePlatformVertically()
    {
	// If platform can be created under startGenerationAt
	if (lastGeneratedObjectY + platformMaxHeightDelta < startGenerationAt + juppy.SessionHeightScore)
	{
	    float newPlatformX;
	    float newPlatformY = lastGeneratedObjectY + platformMaxHeightDelta;

	    newPlatformX = rnd.Next((int)lastGeneratedObjectX - platformMaxWidthDelta, (int)lastGeneratedObjectX + platformMaxWidthDelta);


	    GameObject platformToCreate = GetRandomPlatform();

	    Instantiate(platformToCreate, new Vector2(newPlatformX, newPlatformY), platformToCreate.transform.rotation);

	    lastGeneratedObjectX = newPlatformX;

	    lastGeneratedObjectY = newPlatformY;

	    AttemptGenerateHeart(lastGeneratedObjectX, lastGeneratedObjectY);
	}
    }

    void AttemptGenerateHeart(float x, float y)
    {
	if (rnd.Next(0, 50) == 0)
	{
	    float heartX = rnd.Next((int)x - 400, (int)x + 400);
	    float heartY = rnd.Next((int)y - 400, (int)y + 400);

	    Instantiate(heart, new Vector2(heartX, heartY), heart.transform.rotation);
	}
    }

    GameObject CreateMoodKiller()
    {
	lastGeneratedObjectX = rnd.Next((int)lastGeneratedObjectX - platformMaxWidthDelta, (int)lastGeneratedObjectX + platformMaxWidthDelta);

	return Instantiate(GetMoodKiller(), new Vector2(lastGeneratedObjectX, lastGeneratedObjectY), moodKiller.transform.rotation);
    }

    void ClearAllPlatforms()
    {
	// Make all falling platforms fall
	GameObject[] fallingPlatforms = GameObject.FindGameObjectsWithTag("FallingPlatform");
	foreach(GameObject platform in fallingPlatforms){
	    Transform platformParent = platform.transform.parent;

	    MakePlatformSolid(platformParent);
	}

	// Destroy all normal platforms
	GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
	foreach(GameObject platform in platforms){
	    platform.GetComponent<Platform>().Kill();
	}

    }

    void MakePlatformSolid(Transform platform){
	platform.GetComponent<BoxCollider2D>().isTrigger = false;

	platform.GetComponent<Rigidbody2D>().gravityScale = 100;
    }

    void Update()
    {
	if (changePlatformGenerationAxisTimer > changePlatformGenerationAxisTime)
	{
	    if (livingMoodKillerCreated == false)
	    {
		livingMoodKiller = CreateMoodKiller();
		livingMoodKillerCreated = true;
	    }

	    if (livingMoodKiller == null)
	    {
		lastGeneratedObjectX = juppyTransform.position.x;
		lastGeneratedObjectY = juppyTransform.position.y - 100;
		livingMoodKillerCreated = false;
		ClearAllPlatforms();
		// Create platform
		GameObject platform = GetRandomPlatform();
                Instantiate(platform, new Vector2(lastGeneratedObjectX,  lastGeneratedObjectY), platform.transform.rotation);
		generateHorizontally = !generateHorizontally;
		changePlatformGenerationAxisTimer = 0;
	    }
	}
	else
	{
	    changePlatformGenerationAxisTimer++;
	}

	if (platforms.Length != 0)
	{
	    if (livingMoodKillerCreated == false)
	    {
		if (generateHorizontally == true)
		{
		    AttemptGeneratePlatformHorizontally();
		}
		else
		{
		    AttemptGeneratePlatformVertically();
		}
	    }
	}
	else
	{
	    Debug.LogError("ERROR: ingen platform prefab satt på platforms genereraren");
	}

    }
}
