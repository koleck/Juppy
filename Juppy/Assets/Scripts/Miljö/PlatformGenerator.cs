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


    Juppy juppy;

    [SerializeField]
    GameObject platform;

    System.Random rnd = new System.Random();

    Transform platformTransform;

    void Start () {
	platformTransform = GetComponent<Transform>();

	GameObject juppyObjekt = GameObject.FindGameObjectsWithTag("Juppy")[0];
	juppy = juppyObjekt.GetComponent<Juppy>();
    }

    void Update() {

	if(platform != null){
		// If platform can be created under platformGenerationOffset
	    if(lastGeneratedPlatformY + platformVerticalDelta < platformGenerationOffset + juppy.SessionHeightScore){

		float newPlatformX = rnd.Next((int) lastGeneratedPlatformX - platformHorizontalDelta, (int) lastGeneratedPlatformX + platformHorizontalDelta);

		if(newPlatformX < gameScreenStart){
		    newPlatformX = gameScreenStart;
		}
		else if(newPlatformX > gameScreenEnd){
		    newPlatformX = gameScreenEnd;
		}

		Instantiate(platform, new Vector2(newPlatformX,  lastGeneratedPlatformY + platformVerticalDelta), platformTransform.rotation);

		lastGeneratedPlatformY = lastGeneratedPlatformY + platformVerticalDelta;
	    }
	}
	else{
	    Debug.LogError("ERROR: ingen platform prefab satt på platforms genereraren");
	}

    }
}
