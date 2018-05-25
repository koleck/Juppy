using UnityEngine;
using UnityEngine.SceneManagement;


public class MoodKiller : MonoBehaviour {

    [SerializeField]
    int health = 1;
    public static AudioClip moodLjud;
    private AudioSource src3;

    public int Health {get{return health;} set{health = value;}}


    void Update() {
        src3 = GameObject.Find("moodSound").GetComponent<AudioSource>();
	if(Health <= 0){
	    kill();
	}
    }

    void moodKillerSound()
    {
        src3.PlayOneShot(moodLjud, 0.2f);
        src3.Play();
    }

    public void kill(){
        moodKillerSound();
        Destroy(this.gameObject);
    }

}
