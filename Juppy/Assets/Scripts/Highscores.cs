using UnityEngine;
using UnityEngine.UI;

public class Highscores : MonoBehaviour {

    //public static int HighestHeight{get;set;}
    //public static int HighestMoodkillerCount{get;set;}
    //public static int HighestHeartCount{get;set;}

    [SerializeField]
    private Text heightText;

    [SerializeField]
    private Text moodKillerText;

    [SerializeField]
    private Text heartText;

    void Start() {
        
        int height =0;
        height = PlayerPrefs.GetInt("height", height);
        heightText.text = height.ToString();

        int moodkiller = 0;
        moodkiller = PlayerPrefs.GetInt("moodkiller", moodkiller);
        moodKillerText.text = moodkiller.ToString();

	//moodKillerText.text = HighestMoodkillerCount.ToString();

	//heartText.text = HighestHeartCount.ToString();
        int hearts = 0;
        hearts = PlayerPrefs.GetInt("hearts", hearts);
        heartText.text = hearts.ToString();
    }
}
