using UnityEngine;
using UnityEngine.UI;

public class Highscores : MonoBehaviour {

    public static int HighestHeight{get;set;}
    public static int HighestMoodkillerCount{get;set;}
    public static int HighestHeartCount{get;set;}

    [SerializeField]
    private Text heightText;

    [SerializeField]
    private Text moodKillerText;

    [SerializeField]
    private Text heartText;

    void Update() {
	heightText.text = HighestHeight.ToString();

	moodKillerText.text = HighestMoodkillerCount.ToString();

	heartText.text = HighestHeartCount.ToString();
    }
}
