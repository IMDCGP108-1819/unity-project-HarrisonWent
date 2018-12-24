using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    //Used to navigate main menu

    [Header("Menu objects")]
    public GameObject[] Menus;
    public bool[] MenuStates;
    public LoadingScreen loadScreen;

	private string path = "SaveFolder";
	private string file = "SavedData";

    [Header("Tutorial:")]
    public string[] TutorialText;
    public Sprite[] TutorialImages;
    private int tutorialPageCount;
    public Image TutorialDisplay;
    public Text TutorialInfo;

	void Start()
	{
        Cursor.lockState = CursorLockMode.Confined;
        
        //Checks for highscore save folder and file
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
            //Displays highscores on menu top bar
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

    //Scrolls through tutorial pages
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

    //Loads gameplay level in loadscreen script
	public void StartGame()
    {
        loadScreen.StartCoroutine("ScreenSleep");        
    }

    //Toggles given menu item on and off
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

    //Close all pop up menus 
    public void CloseMenus()
    {
        foreach (GameObject a in Menus)
        {
            a.SetActive(false);
        }
    }

    //Quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
