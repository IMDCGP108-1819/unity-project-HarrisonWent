using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Audio;

[RequireComponent(typeof(SkyBoxColour))]

public class GameManager : MonoBehaviour
{
    //Used to manage the gameplay

	public static int wave = 0;
	public static int Difficulty = 0;

    public static bool limitWaves = false;
    public AudioMixer SoundMixer;
    public Text ScoreText;

    private PlanetSpawn PlanetSpawnScript1, PlanetSpawnScript2, PlanetSpawnScript3;
    private NebulaSpawn NebulaSpawnScript;
    private SpawnPlayer PlayerSpawnScript;

    private bool GameOverInProgress = false;

    void Start()
    {
        //Checks for spawning objects and asigns scripts
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
        if(a = GameObject.Find("PowerUpSpawner"))
        {
            PlanetSpawnScript3 = a.GetComponent<PlanetSpawn>();
        }
        else
        {
            Debug.Log("Missing PowerUpSpawner object in scene!");
        }

        if (limitWaves) { wave = 11; }
        //First wave
        NextWave();
    }

    //Spawns objects, random map and player
    public void NextWave()
    {
        Debug.Log("Next wave!");

        if (limitWaves)
        {
            if (wave == 1)
            {
                GameOver();
            }
            else
            {
                wave--;
                ScoreText.text = "PLANETS REMAINING: " + wave;
            }
        }
        else
        {
            wave++;
            ScoreText.text = "WAVE " + wave;
        }
        
        PlanetSpawnScript1.SpawnPlanets();
        PlanetSpawnScript2.SpawnPlanets();
        PlanetSpawnScript3.SpawnPlanets();

        GetComponent<SkyBoxColour>().newColour();
        NebulaSpawnScript.SpawnMap();

		PlayerSpawnScript.Spawn(false);
    }

    //Ends game, loads to main menu
    public void GameOver()
    {
        if (GameOverInProgress) { return; }
        GameOverInProgress = true;

        GameOverStats.wave = wave;
        wave = 0;

        GameObject.Find("CarriedOverSettings").GetComponent<LoadingScreen>().StartCoroutine("ScreenSleep");
    }

    //Delayed start to next wave, updates high scores
	public IEnumerator PrepareForNextWave()
	{
		SaveScore ();
		yield return new WaitForSeconds (2f);
		NextWave ();
	}

    //Checks for high score and updates scores
	private void SaveScore()
	{
        Debug.Log("Update score");
        int Easy = 0, Normal = 0, Hard = 0;
		string path = "SaveFolder";
		string file = "SavedData";

		if (File.Exists (Application.persistentDataPath + "\\" + path + "\\" + file)) 
		{
			StreamReader reader = new StreamReader (Application.persistentDataPath + "\\" + path + "\\" + file);

            //Different scores for different difficulty
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

        //Writes new highscores
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

    //Slows time down and speeds it back up
    public IEnumerator TimeDown()
    {

        for (float a = 1.00f; a > 0.25f; a -= 0.05f)
        {
            Time.timeScale = a;
            SoundMixer.SetFloat("MasterPitch", a);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        for (float a = 0.25f; a < 1.00f; a += 0.05f)
        {
            Time.timeScale = a;
            SoundMixer.SetFloat("MasterPitch", a);
            yield return null;
        }
    }
}
