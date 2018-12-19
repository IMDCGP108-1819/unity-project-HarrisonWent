using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private int wave = 0;
	public bool HitTarget = false;
	public int Difficulty = 0;
    public Text ScoreText;

    void Start()
    {
        NextWave();
    }

    public void NextWave()
    {		
		wave++;
		ScoreText.text = "WAVE " + wave;

		HitTarget = false;

        GameObject.Find("PlanetSpawner").GetComponent<PlanetSpawn>().SpawnPlanets();
		GameObject.Find("ObstacleSpawner").GetComponent<PlanetSpawn>().SpawnPlanets();
		GameObject.Find("SkyBoxSpawn").GetComponent<NebulaSpawn>().SpawnMap();	

		GameObject.Find("PlayerSpawn").GetComponent<SpawnPlayer>().Spawn(false);
    }

    public void GameOver()
    {
		SceneManager.LoadSceneAsync(0);
    }

	public IEnumerator PrepareForNextWave()
	{
		HitTarget = true;
		SaveScore ();
		yield return new WaitForSeconds (2);
		NextWave ();
	}

	private void SaveScore()
	{
		string path = "SaveFolder";
		string file = "SavedData";

		if (File.Exists (Application.persistentDataPath + "\\" + path + "\\" + file)) 
		{
			StreamReader reader = new StreamReader (Application.persistentDataPath + "\\" + path + "\\" + file);
			string line = reader.ReadLine ();

			if (wave < int.Parse (line)) 
			{
				reader.Close();
				return;
			}

			reader.Close();

			File.Delete (Application.persistentDataPath + "\\" + path + "\\" + file);
		}

		var data = File.CreateText (Application.persistentDataPath + "\\" + path + "\\" + file);
		data.WriteLine ((wave).ToString());
		Debug.Log (wave);
		data.Close();

	}
}
