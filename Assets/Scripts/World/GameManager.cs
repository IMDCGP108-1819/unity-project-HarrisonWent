using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SkyBoxColour))]

public class GameManager : MonoBehaviour
{
	private static int wave = 0;
	public static int Difficulty = 0;

    public Text ScoreText;
    private PlanetSpawn PlanetSpawnScript1, PlanetSpawnScript2;
    private NebulaSpawn NebulaSpawnScript;
    private SpawnPlayer PlayerSpawnScript;

    void Start()
    {
        GameObject a;
        if (a = GameObject.Find("PlanetSpawner"))
        {
            PlanetSpawnScript1 = a.GetComponent<PlanetSpawn>();
        }
        else
        {
            Debug.Log("Missing PlanetSpawner object in scene!");
        }
        if (a = GameObject.Find("ObstacleSpawner"))
        {
            PlanetSpawnScript2 = a.GetComponent<PlanetSpawn>();
        }
        else
        {
            Debug.Log("Missing ObstacleSpawner object in scene!");
        }
        if (a = GameObject.Find("SkyBoxSpawn"))
        {
            NebulaSpawnScript = a.GetComponent<NebulaSpawn>();
        }
        else
        {
            Debug.Log("Missing SkyBoxSpawn object in scene!");
        }
        if(a = GameObject.Find("PlayerSpawn"))
        {
            PlayerSpawnScript = a.GetComponent<SpawnPlayer>();
        }
        else
        {
            Debug.Log("Missing PlayerSpawn object in scene!");
        }
        
        NextWave();
    }

    public void NextWave()
    {
        Debug.Log("Next wave!");

		wave++;
		ScoreText.text = "WAVE " + wave;

        GetComponent<SkyBoxColour>().newColour();
        PlanetSpawnScript1.SpawnPlanets();
        PlanetSpawnScript2.SpawnPlanets();
		NebulaSpawnScript.SpawnMap();
		PlayerSpawnScript.Spawn(false);
    }

    public void GameOver()
    {
        wave = 0;
		SceneManager.LoadSceneAsync(0);
    }

	public IEnumerator PrepareForNextWave()
	{
        Debug.Log("Prepare for next wave...");

		SaveScore ();
		yield return new WaitForSeconds (2f);
		NextWave ();
	}

	private void SaveScore()
	{
        Debug.Log("Update score");
        int Easy = 0, Normal = 0, Hard = 0;
		string path = "SaveFolder";
		string file = "SavedData";

		if (File.Exists (Application.persistentDataPath + "\\" + path + "\\" + file)) 
		{
			StreamReader reader = new StreamReader (Application.persistentDataPath + "\\" + path + "\\" + file);
			string lineEasy = reader.ReadLine ();
            string lineNormal = reader.ReadLine();
            string lineHard = reader.ReadLine();

            if (lineEasy != null)
            {
                if (Difficulty == 0 & wave < (Easy = int.Parse(lineEasy)))
                {
                    reader.Close();
                    return;
                }
                if (Difficulty == 1 & wave < (Normal = int.Parse(lineNormal)))
                {
                    reader.Close();
                    return;
                }
                if (Difficulty == 2 & wave < (Hard = int.Parse(lineHard)))
                {
                    reader.Close();
                    return;
                }
            }
            Debug.Log("New high score!");
			reader.Close();

			File.Delete (Application.persistentDataPath + "\\" + path + "\\" + file);
		}

		var data = File.CreateText (Application.persistentDataPath + "\\" + path + "\\" + file);
        switch (Difficulty)
        {
            case 0:
                data.WriteLine(wave.ToString());
                data.WriteLine(Normal.ToString());
                data.WriteLine(Hard.ToString());
                break;
            case 1:
                data.WriteLine(Easy.ToString());
                data.WriteLine(wave.ToString());
                data.WriteLine(Hard.ToString());
                break;
            case 2:
                data.WriteLine(Easy.ToString());
                data.WriteLine(Normal.ToString());
                data.WriteLine(wave.ToString());
                break;

        }

		Debug.Log (wave);
		data.Close();

	}
}
