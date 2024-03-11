using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public GameObject pauseMenu;
	public GameObject gameOverMenu;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//uses the p button to pause and unpause the game
		if (Input.GetKeyDown(KeyCode.P))
		{
			if (Time.timeScale == 1)
			{
				showPaused();
			}
			else if (Time.timeScale == 0)
			{
				hidePaused();
			}
		}
	}

	public void GameOver()
    {
		gameOverMenu.SetActive(true);
		Time.timeScale = 0;
		pauseControl();

	}

	//Reloads the Level
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	//controls the pausing of the scene
	public void pauseControl()
	{
		if (Time.timeScale == 1)
		{
			showPaused();
		}
		else
		{
			hidePaused();
		}
	}

	//shows objects with ShowOnPause tag
	public void showPaused()
	{
		pauseMenu.SetActive(true);
		Time.timeScale = 0;
	}

	//hides objects with ShowOnPause tag
	public void hidePaused()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}

	//loads inputted level
	public void LoadLevel(string level)
	{
		SceneManager.LoadScene(level);
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
