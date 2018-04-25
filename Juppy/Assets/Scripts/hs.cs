
using UnityEngine;
using UnityEngine.UI;

public class hs : MonoBehaviour
{

    public Text score;
    int hearts;
    void updateScore()
    {
        score.text = hearts.ToString();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
            Destroy(gameObject);
            hearts++;

    }
}
