using UnityEngine;
using System.Collections;
// This controls the action after cube or circle touches the car. 
public class PlayerController : MonoBehaviour {
	public float fuel = 100f;

	GameController gc;

	void Start(){
		gc = FindObjectOfType<GameController>();
	}

	void Update(){
		if(gc.GameCanvas.activeSelf){
			fuel -= 10 * Time.deltaTime;
		}

		if(fuel < 0)
		{
			GameObject.FindGameObjectWithTag("GameController").SendMessage("GameOver");
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		//On circle touching the car, it will play scoreUp audio & send message to gamecontroller to add score .
	if(other.gameObject.tag == "Circle"){
		Destroy(other.gameObject);
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();
			GameObject.FindGameObjectWithTag("GameController").SendMessage("AddScore");
			fuel += Random.Range(5, 8);
	}
		//On cube touching the car, it will send message to gamecontroller to end the game.
	if(other.gameObject.tag == "Cube"){
		GameObject.FindGameObjectWithTag("GameController").SendMessage("GameOver");
	}
}
}