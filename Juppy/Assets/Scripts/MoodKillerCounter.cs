using UnityEngine;
using UnityEngine.UI;

public class MoodKillerCounter : MonoBehaviour
{

    public Text text;

    [SerializeField]
    Juppy juppy;

    void Start()
    {
        text = this.GetComponent<Text>();
        juppy = GameObject.FindGameObjectsWithTag("Juppy")[0].GetComponent<Juppy>();
    }

    void Update()
    {
        text.text = juppy.MoodKillersDefeated.ToString();
    }
}
