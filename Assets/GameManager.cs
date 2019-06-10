
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	bool gameHasEnded = false;
	public float losingDelay = 2.0f;
	public float winningDelay = 5.0f;
	public GameObject GameOverPanel;
	public GameObject YouWinPanel;
    public GameObject UpgradeScreen;

    public float LevelMinX = 0.0f;
    public float LevelMaxX = 1283.0f;
    public float LevelMinZ = 0.0f;
    public float LevelMaxZ = 1283.0f;

    private bool lockCursor = true;

    //Insert Snippet Into Village Damage Method
    //================================
    // public GameManager gameManager;
    //
    //if (villageDamage <= 0)
    //{
    //	GameManager.YouWin ();
    //}
    //================================

    public void Update()
    {
        if ( Input.GetKeyDown( KeyCode.Tab ) )
        {
			lockCursor = !lockCursor;

            UpgradeScreen.SetActive(!UpgradeScreen.activeInHierarchy);
            Time.timeScale = UpgradeScreen.activeInHierarchy ? 0 : 1;
            foreach(driver d in Resources.FindObjectsOfTypeAll<driver>())
            {
                d.enabled = !UpgradeScreen.activeInHierarchy;
            }
        }

		if( GameObject.FindGameObjectsWithTag( "Village Building" ).Length == 0 )
		{
			YouWin();
		}

		// locks/unlocks the cursor
		Screen.lockCursor = lockCursor;
    }

    //You Win
    public void YouWin()
	{
		YouWinPanel.SetActive(true);
		Invoke( "MainMenu", winningDelay );
	}

	//You Lose
	public void EndGame ()
	{
		if (gameHasEnded == false)
		{
			gameHasEnded = true;
			Debug.Log ("GAME OVER");
			GameOverPanel.SetActive(true);
			Invoke("Restart", losingDelay);
		}
	}

	void Restart()
	{
		SceneManager.LoadScene ("LevelTest");

		//For Multiple Scenes
		// SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void MainMenu()
	{
		Screen.lockCursor = false;
		SceneManager.LoadScene( "Main Menu" );
	}
}
