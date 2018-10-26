using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {


	public void PlayGame()
	{
		SceneManager.LoadScene("playTest1");
	}

    public void EndGame()
    {
        SceneManager.LoadScene("EndMenu");
    }

    public void EndGame2()
    {
        SceneManager.LoadScene("EndMenu2");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
