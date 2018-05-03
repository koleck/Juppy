using UnityEngine;
using UnityEngine.UI;

public class HeightCounter : MonoBehaviour {

    [SerializeField]
    private Text text;

    float lastHeight;

    [SerializeField]
    Juppy juppy;

    void Start () {
	text = this.GetComponent<Text>();
	juppy = GameObject.FindGameObjectsWithTag("Juppy")[0].GetComponent<Juppy>();
    }

    void Update() {
	int height = (int) juppy.SessionHeightScore ;
	text.text = height.ToString();
    }

}
