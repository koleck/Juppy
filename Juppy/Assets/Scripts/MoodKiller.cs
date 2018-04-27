using UnityEngine;
using UnityEngine.SceneManagement;

public class MoodKiller : MonoBehaviour {

    [SerializeField]
    int health = 2;

    public int Health {get{return health;} set{health = value;}}

    void Update() {
	if(Health <= 0){
	    Destroy(this.gameObject);
	}
    }

}
