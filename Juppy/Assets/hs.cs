
using UnityEngine;
using UnityEngine.UI;

public class hs : MonoBehaviour {

    public Text score;
    int hearts;
    void updateScore(){
        score.text = hearts.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Destroy(gameObject);
            hearts++;
        }
    }
}
