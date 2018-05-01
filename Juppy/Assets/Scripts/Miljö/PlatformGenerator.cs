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
    bool generateHorizontally = false;

    Juppy juppy;


    int moodKillerHeight = 500;

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
	}
    }

    void FixMe(){


	if(rnd.Next(0, 20) == 0){
	    lastGeneratedObjectX = rnd.Next((int) lastGeneratedObjectX - objectSecondaryDistanceDelta, (int) lastGeneratedObjectX + objectSecondaryDistanceDelta);

	    //	    Instantiate(heart, new Vector2(lastGeneratedObjectX, lastGeneratedObjectY + objectPrimaryDistanceDelta), objectToSpawn.transform.rotation);
	}
    }

    void CreateMoodKiller(){
	lastGeneratedObjectX = rnd.Next((int) lastGeneratedObjectX - objectSecondaryDistanceDelta, (int) lastGeneratedObjectX + objectSecondaryDistanceDelta);

	Instantiate(moodKiller, new Vector2(lastGeneratedObjectX, lastGeneratedObjectY + moodKillerHeight), moodKiller.transform.rotation);
    }

    void Update() {
	if(changePlatformGenerationAxisTimer > changePlatformGenerationAxisTime)
	{
	    CreateMoodKiller();
	    generateHorizontally = !generateHorizontally;
	    changePlatformGenerationAxisTimer = 0;
	}
	else{
	    changePlatformGenerationAxisTimer++;
	}

	if(platforms.Length != 0){
	    if(generateHorizontally == true){
		AttemptGeneratePlatformHorizontally();
	    }
	    else {
		AttemptGeneratePlatformVertically();
	    }
	}
	else{
	    Debug.LogError("ERROR: ingen platform prefab satt på platforms genereraren");
	}

    }
}
