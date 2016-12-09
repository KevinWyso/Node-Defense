﻿using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {

	public GameObject gameoverUI;
	public GameObject yes, no, toMenu;

	[HideInInspector]
	public static bool gameEnded;

	void Awake(){
		gameEnded = false; //Need to do this on scene start or static variable will be preserved
		gameObject.GetComponent<activeBuffs> ().applyNodeBuffs(); //Apply node buffs at start
	}

	void Start(){
		gameObject.GetComponent<activeBuffs> ().applyEconBuffs (); //Apply econ buffs at start but after Node buffs (can't change gameStats before they are created)
	}

	// Update is called once per frame
	void Update () {
		//Check framerate
		//Debug.Log (Mathf.FloorToInt(1.0f / Time.deltaTime));
		if (gameEnded)
			return;

		//Debug Statement
		if(Input.GetKeyDown("e")){
			gameStats.waves += 5000;
			endGame();
		}

		if (gameStats.lives <= 0) {
			endGame ();
		}
	}

	void endGame(){
		waveSpawner.enemiesAlive = 0;
		gameEnded = true;
		gameoverUI.SetActive(true);

		//Save the players stats
		playerStats.current.addExpPerDiff (gameStats.waves);
		playerStats.current.setLevel ();

		//IF the player is using a saved file, save it at that index, if not, don't save
		if(playerStats.saveIndex >= 0)
			SaveLoad.Save (playerStats.saveIndex);
	}

	//Confirm the user wants to use the menu
	public void openConfirmation(){
		yes.SetActive (true);
		no.SetActive (true);
		toMenu.SetActive (false);
	}

	//Close in the even that the user does not want to go to the menu
	public void closeConfirmation(){
		yes.SetActive (false);
		no.SetActive (false);
		toMenu.SetActive (true);
	}

		
}