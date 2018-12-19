using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawn : MonoBehaviour {

    public GameObject[] Planets;
    public int countToSpawn = 1000;
	public List<GameObject> SpawnedPlanetList;
	public bool increasePerWave;

    public void SpawnPlanets()
    {

		if (SpawnedPlanetList != null) 
		{
			Debug.Log ("Delete contents of list");

			foreach (GameObject obj in SpawnedPlanetList) 
			{
				Destroy (obj);
			}
		}

		if (increasePerWave)
		{
			countToSpawn++;
		}

		Debug.Log("Spawn planets!");

        for (int count = 0; count < countToSpawn; count++)
        {
            Vector3 RandomPosition = new Vector3(Random.Range(transform.position.x - 3, transform.position.x + 3), Random.Range(transform.position.y - 5, transform.position.y + 5), 0);
			GameObject Spawn = GameObject.Instantiate (Planets [Random.Range (0, Planets.Length)], RandomPosition, transform.rotation);
			SpawnedPlanetList.Add(Spawn);
        }
    }
}
