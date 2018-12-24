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
            score.text = "WAVES COMPLETED: " + (wave-1);
        }
    }

    //Closes stat screen
    public void closeScore()
    {
        Destroy(this.gameObject);
    }
}
