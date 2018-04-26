using UnityEngine;

public class HeartProjectile : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
	if(other.tag == "MoodKiller"){
	    MoodKiller moodKiller = other.GetComponent<MoodKiller>();
	    moodKiller.Health = moodKiller.Health - 1;
	    Destroy(this.gameObject);
	}
    }
}
