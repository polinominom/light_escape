using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	public bool isGameOver;
	public AudioClip startButtonAudio;

	private GameObject timer;
	private GameObject timeText;
	private GameObject topBar;
	private GameObject MenuBackground;
	private GameObject ManInBlack;
	private GameObject LevelButtonImage;
	private GameObject OptionsButtonImage;
	private GameObject StartButtonImage;
	private GameObject stoneCountText;
	private GameObject stoneImage;
	private GameObject whistleImage;
	private GameObject whistleCountText;
	private GameObject LightEscapeText;
	private GameObject teleportCountText;
	private GameObject teleportCountImage;

	private Image fillImg;

	public float timeAmt;
	public int whistleCount;
	public int stoneCount;
	public int teleportCount;
	public int invisibleCount;
	public int disguiseCount;
	private float time = 5;
	
	private bool paused;
	private bool gameEnd;
	public bool playAnimation;
	private int MenuAppearStart; 
	private bool playGame = false; 
	private bool restartGame = false;

	private GameObject gameOverText;

	private Transform winGame;
	private Transform startButton;

	private GameObject optionMenu;
	private GameObject mainMenu;

	void Awake()
	{
		gameOverText = GameObject.Find ("GameOver");
		timer = GameObject.Find ("Timer");
		fillImg = timer.GetComponent<Image>();
		timeText = GameObject.Find ("TimeText");
		topBar = GameObject.Find ("TopBar");
		MenuBackground = GameObject.Find ("MenuBackground");
		ManInBlack = GameObject.Find ("BG_Man");
		OptionsButtonImage = GameObject.Find ("OptionsButton");
		LevelButtonImage = GameObject.Find ("LevelsButton");
		StartButtonImage = GameObject.Find ("StartButton");
		stoneCountText = GameObject.Find ("StoneText");
		whistleCountText = GameObject.Find ("WhistleText");	
		stoneImage = GameObject.Find ("StoneImage");
		whistleImage = GameObject.Find ("WhistleImage");	
		LightEscapeText = GameObject.Find ("LightEscapeText");

		teleportCountText = GameObject.Find ("TeleportCountText");
		teleportCountImage = GameObject.Find ("TeleportCountImage");	

		winGame = GameObject.Find ("WinGame").transform;
		startButton = GameObject.Find ("StartButton").transform;

		optionMenu = GameObject.Find ("OptionMenu");
		mainMenu = GameObject.Find ("MainMenu");
	}

	void Start () 
	{
		isGameOver = false;

		topBar.GetComponent<CanvasRenderer>().SetAlpha(0.8f);	
		timeText.GetComponent<Text> ().text = time.ToString ("0");
		
		LightEscapeText.GetComponent<CanvasRenderer>().SetAlpha(0.01f);	
		MenuBackground.GetComponent<CanvasRenderer>().SetAlpha(0.01f);	
		ManInBlack.GetComponent<CanvasRenderer>().SetAlpha(0.01f);	
		OptionsButtonImage.GetComponent<CanvasRenderer>().SetAlpha(0.01f);	
		StartButtonImage.GetComponent<CanvasRenderer>().SetAlpha(0.01f);	
		LevelButtonImage.GetComponent<CanvasRenderer> ().SetAlpha (0.01f);

		ActivateStartMenu (true);

		LightEscapeText.GetComponent<Text> ().CrossFadeAlpha (1f, 1f, false);

		MenuBackground.GetComponent<Image> ().CrossFadeAlpha (0.7f, 1f, false);
		ManInBlack.GetComponent<Image> ().CrossFadeAlpha (1f, 1f, false);
			
		StartButtonImage.GetComponent<Image> ().CrossFadeAlpha (1f, 1f, false);
		OptionsButtonImage.GetComponent<Image> ().CrossFadeAlpha (1f, 1f, false);
		LevelButtonImage.GetComponent<Image> ().CrossFadeAlpha (1f, 1f, false);
 
		time = timeAmt;
		MenuAppearStart = 0;
		paused = false;
		gameEnd = false;
		playAnimation = true;

		ActivateHud (false);
	}

	void Update () {
		if (playGame)
		{
			//TODO: adjust text in order to selected skills;
			stoneCountText.GetComponent<Text> ().text = "Stone: " + stoneCount.ToString ();
			whistleCountText.GetComponent<Text> ().text = "Whistle: " + whistleCount.ToString ();
			teleportCountText.GetComponent<Text> ().text = "Teleport: " + teleportCount.ToString (); 
					
			if (time > 0.0f) {
				time -= Time.deltaTime;
				fillImg.fillAmount = time / timeAmt;
				timeText.GetComponent<Text> ().text = time.ToString ("0");
				if (time < 0.0f) {
					time = 0.0f;
					timeText.GetComponent<Text> ().text = time.ToString ("0");
				}
			}
			if (time == 0.0f && playAnimation) {
							
				StartCoroutine (GameOverAnimation ());
				playAnimation = false;
			} 
		}	
	}

	void ActivateHud(bool val)
	{
		stoneCountText.GetComponent<Text> ().enabled = val;
		whistleCountText.GetComponent<Text> ().enabled = val;
		teleportCountText.GetComponent<Text> ().enabled = val;

		whistleImage.GetComponent<Image> ().enabled = val;
		stoneImage.GetComponent<Image> ().enabled = val;
		teleportCountImage.GetComponent<Image> ().enabled = val;		
	}

	void ActivateStartMenu(bool val)
	{
		Debug.Log ("Activate Start Menu has been clicked: "+val.ToString());
		MenuBackground.GetComponent<Image> ().enabled = val;
		ManInBlack.GetComponent<Image> ().enabled = val;
		StartButtonImage.GetComponent<Image> ().enabled = val;
		OptionsButtonImage.GetComponent<Image> ().enabled = val;
		LevelButtonImage.GetComponent<Image> ().enabled = val;

		LightEscapeText.GetComponent<Text> ().enabled = val;		
		topBar.GetComponent<Image> ().enabled = !val;
	}

	public IEnumerator GameOverAnimation()
	{
		playGame = false;		
		gameOverText.GetComponent<Text> ().enabled = true;
		gameOverText.GetComponent<Animator> ().Play ("GameOverAnim");
		yield return new WaitForSeconds(1.5f); // wait for two seconds.
		Debug.Log("GameOverAnimation entered!");
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public IEnumerator YouWinAnimation()
	{
		// start winning animation
		// 10 = win text
		winGame.GetComponent<Text>().enabled = true;
		winGame.GetComponent<Animator>().Play("GameOverAnim");
		yield return new WaitForSeconds (1.5f);
		Debug.Log ("You Win Animation Has Ended!");

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void StartGame()
	{
		Debug.Log ("START GAME HAS BEEN CLICKED");
		// 8th child is "StartGame Button" = 
		startButton.GetComponent<AudioSource> ().PlayOneShot(startButtonAudio);

		//inform the ui skill button controller
		GetComponent<UISkillContoller>().StartGame();

		playGame = true;
		gameOverText.GetComponent<Text> ().enabled = false;
		ActivateStartMenu (false);

		ActivateHud (true); 
	}

	public void PermaDisableSkill(string skillName)
	{
		UISkillContoller skillContoller = GetComponent<UISkillContoller> ();
		skillContoller.PermaDisableButton (skillName);
	}

	public void OnOptionClick()
	{
		mainMenu.SetActive (false);
		optionMenu.SetActive (true);

	}



}

