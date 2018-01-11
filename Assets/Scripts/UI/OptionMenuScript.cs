using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenuScript : MonoBehaviour 
{
	/*Rect transforms*/
	private RectTransform canvasRT;
	private RectTransform backgroundRT;
	private RectTransform skillInfoRT;
	private RectTransform skillTextRT;

	/* Upper Buttons*/
	private Button easyButton;
	private Button hardButton;

	/*Bottom Buttons*/
	private Button backToMenu;

	private Button whistleSkill;
	private Button rockThrowSkill;
	private Button teleportSkill;
	private Button invisibleSkill;
	private Button disguiseSkill;

	private GameObject mainMenu;

	private OptionManager optionManager;
	private int[] selectedSkills;
	private Button[] buttons;

	void Awake()
	{
		canvasRT = GameObject.Find ("Canvas").GetComponent<RectTransform> ();
		backgroundRT = GameObject.Find ("OptionBackground").GetComponent<RectTransform> ();
		skillInfoRT = GameObject.Find ("SkillInfo").GetComponent<RectTransform> ();
		skillTextRT = GameObject.Find ("SkillText").GetComponent<RectTransform> ();

		easyButton = GameObject.Find ("EasyButton").GetComponent<Button> ();
		hardButton = GameObject.Find ("HardButton").GetComponent<Button> ();

		backToMenu = GameObject.Find  ("OptionBackButton").GetComponent<Button> ();

		whistleSkill = GameObject.Find  ("WhistleSelectableSkill").GetComponent<Button> ();
		rockThrowSkill = GameObject.Find  ("RockSelectableSkill").GetComponent<Button> ();
		teleportSkill = GameObject.Find  ("TeleportSelectableSkill").GetComponent<Button> ();
		invisibleSkill = GameObject.Find  ("InvisibleSelectableSkill").GetComponent<Button> ();
		disguiseSkill = GameObject.Find  ("DisguiseSkill").GetComponent<Button> ();

		mainMenu = GameObject.Find ("MainMenu");

		optionManager = GameObject.Find ("OptionManager").GetComponent<OptionManager> ();
	}

	void Start () 
	{
		RectTransform rt = GetComponent<RectTransform> ();

		Vector2 canvasSize = canvasRT.sizeDelta;

		AdjustUpperSide (canvasSize);

		// adjust the skill text(middle)
		skillTextRT.anchoredPosition = new Vector2 (-1 * (canvasSize.x / 2f - 75), 0f);

		AdjustBottomSide (canvasSize);

		// initialize arrays
		selectedSkills = new int[3];
		selectedSkills [0] = 0;
		selectedSkills [1] = 1;
		selectedSkills [2] = 2;

		buttons = new Button[5];
		buttons [0] = whistleSkill;
		buttons [1] = rockThrowSkill;
		buttons [2] = teleportSkill;
		buttons [3] = invisibleSkill;
		buttons [4] = disguiseSkill;

		UpdateBackgrounds ();
		UpdateText ();

	}
	
	void AdjustUpperSide(Vector2 canvasSize)
	{
		// assing the new width of background and skillInfo
		backgroundRT.sizeDelta = canvasSize * 2f;
		skillInfoRT.sizeDelta = canvasSize;

		// compute the positions of the easy and hard mode buttons(upper side)
		float y = canvasSize.y / 2f;
		float yModifier = GetUpperModifier (canvasSize);

		float x = 0f;
		float xModifier = 0f;

		easyButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x, y - yModifier);
		hardButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x, y - yModifier * 2);		
	}

	void AdjustBottomSide(Vector2 canvasSize)
	{
		// adjust the bottom side
		float y = -1*(canvasSize.y / 2f);
		float yModifier = GetBottomModifier (canvasSize);

		float x = -1 * (canvasSize.x / 2f);
		float xModifier = GetXModifier (canvasSize);

		float bym = y - yModifier * 3;
		float w = whistleSkill.GetComponent<RectTransform> ().sizeDelta.x;

		whistleSkill.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x + xModifier + w / 2f, bym);
		rockThrowSkill.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x + xModifier * 2 + (3 * w / 2f), bym);
		teleportSkill.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x + xModifier * 3 + (5 * w / 2f), bym);
		invisibleSkill.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x + xModifier * 4 + (7 * w / 2f), bym);
		disguiseSkill.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x + xModifier * 5 + (9 * w / 2f), bym);

		x = 0f;
		skillInfoRT.anchoredPosition = new Vector2 (x, y - yModifier*2);

		backToMenu.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x, y - yModifier);

		gameObject.SetActive (false);		
	}


	/* 3x + 2h = y (find x)*/
	float GetUpperModifier(Vector2 canvasSize)
	{
		float y = canvasSize.y / 2f;

		float heightEasy = easyButton.GetComponent<RectTransform> ().sizeDelta.y;
		float heightHard = hardButton.GetComponent<RectTransform> ().sizeDelta.y;

		float result = (y - heightEasy - heightHard)/3f;

		return result;
	}

	/* 4x + h1 + h2 + h3 = y (find x)*/
	float GetBottomModifier(Vector2 canvasSize)
	{
		float y = canvasSize.y / 2f;

		float h1 = whistleSkill.GetComponent<RectTransform> ().sizeDelta.y;
		float h2 = skillInfoRT.sizeDelta.y;
		float h3 = backToMenu.GetComponent<RectTransform> ().sizeDelta.y;

		float result = (y - h1 - h2 - h3)/4f;

		return result;
	}

	/*6x + 5w = total_width*/
	float GetXModifier(Vector2 canvasSize)
	{
		float totalWidth = canvasSize.x;
		float w = whistleSkill.GetComponent<RectTransform> ().sizeDelta.x;
		float result = (totalWidth - 5f * w) / 6f;

		return result;
	}
		
	/* 
	 * 0: whistleButton
	 * 1: rockButton
	 * 2: Teleport
	 * 3: Invisible
	 * 4: Disguise
	*/
	public void SelectSkill(int skillNo)
	{
		bool finished = false;

		// check if there is an empty non selected button
		for (int i = 0; i < 3; i++) 
		{
			if (selectedSkills [i] == -1) 
			{
				// check if skillNo is already full
				bool full = false;
				for (int j = 0; j < 3; j++) {
					if(selectedSkills[j] == skillNo){
						full = true;
						break;
					}
				}

				if (!full) {
					selectedSkills [i] = skillNo;
					UpdateBackgrounds ();
					UpdateText ();
				}
				return;
			}
		}


		//check if the player unselected a button
		for (int i = 0; i < 3; i++) 
		{
			if (selectedSkills [i] == skillNo) 
			{
				selectedSkills [i] = -1;
				UpdateBackgrounds ();
				UpdateText ();
				return;
			}				
		}

		// player selected a different button, so earese the last select and select the new choise.
		selectedSkills[2] = skillNo;
		UpdateBackgrounds ();
		UpdateText ();

	}

	void UpdateBackgrounds()
	{	// find the selected buttons
		int s1 = -1;
		int s2 = -1;
		int s3 = -1;
		for (int i = 0; i < 5; i++) {
			if (s1 != -1 && s2 != -1 && s3 != -1) {
				break;	
			}

			for (int j = 0; j < 3; j++) {
				if (selectedSkills [j] == i) {
					if (s1 == -1) {
						s1 = i;
						break;

					} else if (s2 == -1) {
						s2 = i;
						break;
					} else if (s3 == -1) {
						s3 = i;
						break;
					}
				}
			}
		}


		// open and close the blue selected area
		for (int i = 0; i < 5; i++) 
		{
			if (i == s1 || i == s2 || i == s3) {
				buttons [i].transform.GetChild (0).GetComponent<Image> ().enabled = true;
			} else {
				buttons [i].transform.GetChild (0).GetComponent<Image> ().enabled = false;
			}
		}
	}

	void UpdateText()
	{
		string skillone = GetName (selectedSkills [0]);
		string skilltwo = GetName (selectedSkills [1]);
		string skillthree = GetName (selectedSkills [2]);
		skillInfoRT.GetComponent<Text> ().text = "Next Game Player Have \n[" + skillone + "], [" + skilltwo + "] and [" + skillthree + "] skills.";
	}

	string GetName(int i)
	{
		string result = "";
		switch (i) 
		{
			case 0:
				result = "Whistle";
				break;
			case 1:
				result = "Rock";
				break;

			case 2:
				result = "Teleport";
				break;
			case 3:
				result = "Invisibility";
				break;
			case 4:
				result = "Disguise";
				break;
			default:
				break;
		}

		return result;
	}

	public void OnBackPressed()
	{
		optionManager.selectedButtons = selectedSkills;
		mainMenu.gameObject.SetActive (true);
		gameObject.SetActive (false);
	}
		
}
