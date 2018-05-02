using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    [SerializeField]
    float lastGeneratedObjectX= 0;

    [SerializeField]
    float lastGeneratedObjectY = -500;

    [SerializeField]
    int objectPrimaryDistanceDelta = 170;

    [SerializeField]
    int objectSecondaryDistanceDelta = 500;

    [SerializeField]
    float platformGenerationOffset = 1500;

    [SerializeField]
    int yAxisPlatformsMaxDeltaHeight = 300;


    [SerializeField]
    GameObject[] platforms = new GameObject[2];

    [SerializeField]
    GameObject heart;

    [SerializeField]
    GameObject moodKiller;

    [SerializeField]
    GameObject livingMoodKiller;

    bool livingMoodKillerCreated = false;

    [SerializeField]
    bool generateHorizontally = false;

    Juppy juppy;


    int moodKillerHeight = 100;

    System.Random rnd = new System.Random();

    [SerializeField]
    int changePlatformGenerationAxisTime = 1000;

    [SerializeField]
    int changePlatformGenerationAxisTimer = 0;


    void Start () {
	GameObject juppyObjekt = GameObject.FindGameObjectsWithTag("Juppy")[0];
	juppy = juppyObjekt.GetComponent<Juppy>();
    }

    GameObject GetRandomPlatform(){
	return platforms[rnd.Next(0, platforms.Length)];
    }

    void AttemptGeneratePlatformHorizontally () {
	// If last generated platform is left of right side of screen
	if(lastGeneratedObjectX + objectPrimaryDistanceDelta < juppy.SessionHighestXCoordinate + platformGenerationOffset){

	    float newPlatformX = lastGeneratedObjectX + objectPrimaryDistanceDelta;

	    float newPlatformY;

	    newPlatformY = rnd.Next((int) juppy.SessionHeightScore - yAxisPlatformsMaxDeltaHeight,  (int)juppy.SessionHeightScore + yAxisPlatformsMaxDeltaHeight);

	    lastGeneratedObjectX = newPlatformX;
	    lastGeneratedObjectY = newPlatformY;

	    GameObject platformToCreate = GetRandomPlatform();

	    Instantiate(platformToCreate, new Vector2(newPlatformX, newPlatformY), platformToCreate.transform.rotation);

	    AttemptGenerateHeart(lastGeneratedObjectX, lastGeneratedObjectY);

	}
    }


    void AttemptGeneratePlatformVertically () {
	// If platform can be created under platformGenerationOffset
	if(lastGeneratedObjectY + objectPrimaryDistanceDelta < platformGenerationOffset + juppy.SessionHeightScore){

	    float newPlatformX;
	    float newPlatformY = lastGeneratedObjectY + objectPrimaryDistanceDelta;

	    newPlatformX = rnd.Next((int) lastGeneratedObjectX - objectSecondaryDistanceDelta, (int) lastGeneratedObjectX + objectSecondaryDistanceDelta);


	    GameObject platformToCreate = GetRandomPlatform();

	    Instantiate(platformToCreate, new Vector2(newPlatformX, newPlatformY), platformToCreate.transform.rotation);

	    lastGeneratedObjectX = newPlatformX;

	    lastGeneratedObjectY = newPlatformY;

	    AttemptGenerateHeart(lastGeneratedObjectX, lastGeneratedObjectY);
	}
    }

    void AttemptGenerateHeart(float x, float y){
	if(rnd.Next(0, 50) == 0){
	    float heartX = rnd.Next((int)x - 400, (int)x + 400);
	    float heartY = rnd.Next((int)y - 400, (int)y + 400);

	    Instantiate(heart, new Vector2(heartX, heartY), heart.transform.rotation);
	}
    }

    GameObject CreateMoodKiller(){
	lastGeneratedObjectX = rnd.Next((int) lastGeneratedObjectX - objectSecondaryDistanceDelta, (int) lastGeneratedObjectX + objectSecondaryDistanceDelta);

	return Instantiate(moodKiller, new Vector2(lastGeneratedObjectX, lastGeneratedObjectY + moodKillerHeight), moodKiller.transform.rotation);
    }

    void Update() {
	if(changePlatformGenerationAxisTimer > changePlatformGenerationAxisTime)
	{
	    if(livingMoodKillerCreated == false){
		livingMoodKiller = CreateMoodKiller();
		livingMoodKillerCreated = true;
	    }

	    if(livingMoodKiller == null){
		juppy.SessionHeightScore = juppy.gameObject.GetComponent<Transform>().position.y;
		generateHorizontally = !generateHorizontally;
		changePlatformGenerationAxisTimer = 0;
		livingMoodKillerCreated = false;
	    }
	}
	else{
	    changePlatformGenerationAxisTimer++;
	}

	if(platforms.Length != 0){
	    if(livingMoodKillerCreated == false){

		if(generateHorizontally == true){
		    AttemptGeneratePlatformHorizontally();
		}
		else {
		    AttemptGeneratePlatformVertically();
		}
	    }
	}
	else{
	    Debug.LogError("ERROR: ingen platform prefab satt på platforms genereraren");
	}

	}
}
