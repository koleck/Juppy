using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    [SerializeField]
    float lastGeneratedPlatformX= 0;

    float lastGeneratedPlatformY = -500;

    [SerializeField]
    float platformVerticalDelta = 170;

    int platformHorizontalDelta = 500;

    [SerializeField]
    float platformGenerationOffset = 1000;

    [SerializeField]
    int gameScreenStart = -900;

    [SerializeField]
    int gameScreenEnd = 900;

    [SerializeField]
    GameObject[] spawnableObjects = new GameObject[2];

    [SerializeField]
    GameObject heart;

    [SerializeField]
    GameObject moodKiller;


    Juppy juppy;


    System.Random rnd = new System.Random();

    void Start () {

	GameObject juppyObjekt = GameObject.FindGameObjectsWithTag("Juppy")[0];
	juppy = juppyObjekt.GetComponent<Juppy>();
    }

    void Update() {

	if(spawnableObjects.Length != 0){
	    // If platform can be created under platformGenerationOffset
	    if(lastGeneratedPlatformY + platformVerticalDelta < platformGenerationOffset + juppy.SessionHeightScore){

		float newPlatformX = rnd.Next((int) lastGeneratedPlatformX - platformHorizontalDelta, (int) lastGeneratedPlatformX + platformHorizontalDelta);

		if(newPlatformX < gameScreenStart){
		    newPlatformX = gameScreenStart;
		}
		else if(newPlatformX > gameScreenEnd){
		    newPlatformX = gameScreenEnd;
		}

		GameObject objectToSpawn = spawnableObjects[rnd.Next(0, spawnableObjects.Length)];

		Instantiate(objectToSpawn, new Vector2(newPlatformX,  lastGeneratedPlatformY + platformVerticalDelta), objectToSpawn.transform.rotation);

		if(rnd.Next(0, 20) == 0){
		    float heartPositionX = rnd.Next((int) lastGeneratedPlatformX - platformHorizontalDelta, (int) lastGeneratedPlatformX + platformHorizontalDelta);

		    Instantiate(heart, new Vector2(heartPositionX,  lastGeneratedPlatformY + platformVerticalDelta), objectToSpawn.transform.rotation);

		}

		if(rnd.Next(0, 60) == 0){
		    float moodKillerPositionX = rnd.Next((int) lastGeneratedPlatformX - platformHorizontalDelta, (int) lastGeneratedPlatformX + platformHorizontalDelta);

		    Instantiate(moodKiller, new Vector2(moodKillerPositionX,  lastGeneratedPlatformY + platformVerticalDelta), objectToSpawn.transform.rotation);

		}


		lastGeneratedPlatformY = lastGeneratedPlatformY + platformVerticalDelta;
	    }
	}
	else{
	    Debug.LogError("ERROR: ingen platform prefab satt på platforms genereraren");
	}

    }
}
