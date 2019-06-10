using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string newGameScene1;
	public string newGameScene2;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//public void  QuitGame ()
	//{
	//	Application.Quit
	//}

	public void StartAdventure()
	{
		SceneManager.LoadScene(newGameScene1);
	}

	public void HowToPlay()
	{
		SceneManager.LoadScene(newGameScene2);
	}


}
