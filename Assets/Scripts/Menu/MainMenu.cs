using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public GameObject[] Menus;
    public bool[] MenuStates;
    public LoadingScreen loadScreen;
	private string path = "SaveFolder";
	private string file = "SavedData";

    public string[] TutorialText;
    public Sprite[] TutorialImages;
    private int tutorialPageCount;
    public Image TutorialDisplay;
    public Text TutorialInfo;

	void Start()
	{
		if(!Directory.Exists(Application.persistentDataPath + "\\" + path))
		{
			Directory.CreateDirectory (Application.persistentDataPath + "\\" + path);
		}
		if (!File.Exists (Application.persistentDataPath + "\\" + path + "\\" + file)) {
			File.CreateText (Application.persistentDataPath + "\\" + path + "\\" + file);
			GameObject.Find ("Text (Highscore)").GetComponent<Text> ().text = " ";
		} 
		else 
		{
			StreamReader reader = new StreamReader (Application.persistentDataPath + "\\" + path + "\\" + file);
			string line = reader.ReadLine ();
			GameObject.Find ("Text (Highscore)").GetComponent<Text> ().text = "EASY HIGHSCORE: " + line;
            line = reader.ReadLine();
            GameObject.Find("Text (Highscore)").GetComponent<Text>().text += " | NORMAL HIGHSCORE: " + line;
            line = reader.ReadLine();
            GameObject.Find("Text (Highscore)").GetComponent<Text>().text += " | HARD HIGHSCORE: " + line;
            reader.Close();
		}
	}

    public void NextTutorialPage()
    {
        tutorialPageCount += 1;

        if (tutorialPageCount > TutorialText.Length-1)
        {
            tutorialPageCount = 0;
        }

        TutorialDisplay.sprite = TutorialImages[tutorialPageCount];
        TutorialInfo.text = TutorialText[tutorialPageCount];
    }

	public void StartGame()
    {
        loadScreen.StartCoroutine("ScreenSleep");        
    }

    public void OpenMenu(int MenuNumber)
    {
        if (MenuStates[MenuNumber])
        {
            Menus[MenuNumber].SetActive(false);
            MenuStates[MenuNumber] = false;
        }
        else
        {
            Menus[MenuNumber].SetActive(true);
            MenuStates[MenuNumber] = true;
        }        
    }

    public void CloseMenus()
    {
        foreach (GameObject a in Menus)
        {
            a.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
