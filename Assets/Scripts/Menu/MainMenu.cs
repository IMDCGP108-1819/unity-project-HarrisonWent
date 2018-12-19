using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public GameObject[] Menus;
	private string path = "SaveFolder";
	private string file = "SavedData";

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
			GameObject.Find ("Text (Highscore)").GetComponent<Text> ().text = "HIGHSCORE: " + line;
			reader.Close();
		}
	}

	public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OpenMenu(int MenuNumber)
    {
        CloseMenus();
        Menus[MenuNumber].SetActive(true);
    }

    private void CloseMenus()
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
