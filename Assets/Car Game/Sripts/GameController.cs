using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
//This Script controls the complete game play.
public class GameController : MonoBehaviour {
	public GameObject Car;
	public GameObject[] Objects;
	GameObject ObjectGO;
	public float spawnWait;
	public float startWait;
	public float speed;
	public float IncreaseInTimeSpeed;
	float[] XPosition = new float[3] {-2.1f,0f, 2.1f};
	public bool IsGameOver;
	public Text GameScoreText;
	int score=0;
	int highscore;
	public GameObject GameOverCanvas;
	public GameObject GameCanvas;
	public GameObject StartCanvas;
	public Text ScoreText;
	public Text HighScoreText;
	int CurrentPosition;
	// Use this for initialization
	void Start () {
		//this sets timescale to 1 at start.
		Time.timeScale = 1;
		//this derives the value of highscore at start.
		highscore = PlayerPrefs.GetInt("HighScore",0);
		//this disables GameOverCanves and GameCanvas, enables startCanvas.
		GameOverCanvas.SetActive (false);
		GameCanvas.SetActive (false);
		StartCanvas.SetActive (true);
		//set currentposition to 0 i.e. center
		CurrentPosition = 0;
	}
	//this starts the spawn wave of the cubes and circles.
	IEnumerator SpawnWaves ()
	{
		//wait of startWait(int) seconds
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < 100; i++) {
				//Setting Object Position randomly between XPositions.
				float XPos = XPosition[Random.Range(0, XPosition.Length)];
				Vector3 ObjectPosition = new Vector3 (XPos, 6.5f, 0);
				//Randomly choose between cube & circle.
				ObjectGO=Objects[Random.Range (0, Objects.Length)];
				//create object at ObjectPosition.
				Instantiate (ObjectGO, ObjectPosition, Quaternion.identity);
				//Increase TimeScale
				Time.timeScale+=IncreaseInTimeSpeed;
				//wait between next drop
				yield return new WaitForSeconds (spawnWait);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if(IsGameOver==false){
		//Action on left key Pressed
		if (Input.GetKeyDown ("left")){
			LeftButton ();
	}//Action on right key pressed 
		else if (Input.GetKeyDown("right"))
		{
			RightButton();
		}
	}	
}
	
	public void StartGame(){
		//On Start Game button pressed, disable StartCanvas and GameCanvas & starting the spawn of cubes and circles.
		StartCanvas.SetActive (false);
		GameCanvas.SetActive (true);
		StartCoroutine (SpawnWaves ());
		IsGameOver = false;
	}
	public void AddScore(){
		//Add score by 1 and showing that score to GameScoreText.
		score+=1;
		GameScoreText.text= score.ToString ();
	}
	//In CurrentPosiiton 0 represent center, -1 left and 1 right.
	public void LeftButton(){
		//If at center move to left
		if (CurrentPosition == 0) {
			//animate movemenet to -2.1 i.e left
			Car.transform.DOMoveX (-2.1f, 0.5f);
			//set current position to -1 i.e. left.
			CurrentPosition = -1;
			//If at right move to center
		} else if (CurrentPosition == 1) {
			//animate movemenet to 0 i.e center
			Car.transform.DOMoveX (0f, 0.5f);
			//set current position to 0 i.e. center.
			CurrentPosition = 0;
		}
	}
	public void RightButton(){
		//If at center move to right
		if (CurrentPosition == 0) {
			//animate movemenet to 2.1 i.e right
			Car.transform.DOMoveX (2.1f, 0.5f);
			//set current position to 1 i.e. right.
			CurrentPosition = 1;
		} //if at left move to center
		else if (CurrentPosition == -1) {
			//animate movemenet to 0 i.e center
			Car.transform.DOMoveX (0f, 0.5f);
			//set current position to 0 i.e. center.
			CurrentPosition = 0;
		}
	}
	public void Restart(){
		//Reload the game on restart button
		// Application.LoadLevel (0);
		SceneManager.LoadScene("Game");
	}
	public void GameOver(){
		//sets is isGameOver to true
		IsGameOver = true;
		//Plays GameOverSound
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
		//Pause the time
		Time.timeScale = 0;
		//Stop creating cubes and circles.
		StopCoroutine(SpawnWaves ());
		//activate the gameover canvas
		GameOverCanvas.SetActive (true);
		//show the score value on ScoreText
		ScoreText.text = "Score:" + score.ToString();
		//if score is greater than highscore change the stored value of highscore
		if (score > highscore) {
			PlayerPrefs.SetInt ("HighScore", score);
		}
		//show highscore value on HighScoreText
		highscore = PlayerPrefs.GetInt ("HighScore",0);
		HighScoreText.text = "Highscore:" + highscore.ToString ();
	}
}
