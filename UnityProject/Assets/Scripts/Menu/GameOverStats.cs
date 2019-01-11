using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverStats : MonoBehaviour {

    //Used to transfer score from gameplay to main menu

    public GameObject ScoreScreen;
    public Text score;
    public static int wave = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //Displays game over stat screen
    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            ScoreScreen.SetActive(true);
            if (GameManager.limitWaves)
            {
                if (GameManager.wave > 1)
                {
                    score.text = "YOU ARE DEFEATED. YOU FAILED TO DESTROY ALL TARGETS!";
                }
                else
                {
                    score.text = "YOU WIN. THE GALAXY IS YOURS";
                }
            }
            else
            {
                score.text = "WAVES COMPLETED: " + (wave - 1);
            }
        }
    }

    //Closes stat screen
    public void closeScore()
    {
        Destroy(this.gameObject);
    }
}
