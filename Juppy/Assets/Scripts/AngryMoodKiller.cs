using UnityEngine;
using UnityEngine.SceneManagement;

public class AngryMoodKiller : MonoBehaviour {

    [SerializeField]
    int health = 2;


    [SerializeField]
    float speed = 2f;


    Transform juppy;
    public int Health {get{return health;} set{health = value;}}


    void Start(){
	juppy = GameObject.Find("Juppy").GetComponent<Transform>();

    }

    void Update() {
	if(Health <= 0){
	    kill();
	}

	transform.position = Vector2.MoveTowards(transform.position, juppy.position, speed);
    }

    public void kill(){
	Destroy(this.gameObject);
    }

}
