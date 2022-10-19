using UnityEngine;
using System.Collections;
//This script controls the boundry for destroying the cubes or ending the the game.
public class Boundry : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		//On cube entering the boundries Cube prefab will be destroyed.
		if(other.gameObject.tag == "Cube"){
			Destroy(other.gameObject);
		}
		//On circle entering the boundries, it will send message to gamecontroller to end the game.
		if(other.gameObject.tag == "Circle"){
			Destroy(other.gameObject);
			// GameObject.FindGameObjectWithTag("GameController").SendMessage("GameOver");
		}


}
}
