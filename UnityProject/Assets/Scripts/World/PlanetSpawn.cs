using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawn : MonoBehaviour {
    //Used to spawn target planet, obstacles and powerups

    [Header("Objects to spawn:")]
    public GameObject[] Planets;
    public int countToSpawn = 1;

	public List<GameObject> SpawnedPlanetList;

    [Header("Settings:")]
    public bool increasePerWave, RandomiseSize, DoubleForHardDifficulty, RandomiseChance;

    private int sortcount = 0;

    private void Start()
    {
        //Doubles if hard
        if (DoubleForHardDifficulty && GameManager.Difficulty == 2)
        {
            countToSpawn *= 2;
        }
    }

    public void SpawnPlanets()
    {
        //Remove object pool if reset
		if (!increasePerWave) 
		{

			foreach (GameObject obj in SpawnedPlanetList) 
			{
				Destroy (obj);
			}
            sortcount = 0;
		}

        //Chance to spawn at all
        if (RandomiseChance)
        {
            int rand = Random.Range(0, 10);
            if(rand > 4)
            {
                return;
            }
        }

        //Add new object to spawned object pool
        for (int count = 0; count < countToSpawn; count++)
        {
            Vector3 RandomPosition = new Vector3(Random.Range(transform.position.x - 3, transform.position.x + 3), Random.Range(transform.position.y - 5, transform.position.y + 5), 0);
			GameObject Spawn = GameObject.Instantiate (Planets [Random.Range (0, Planets.Length)], RandomPosition, transform.rotation);
            Spawn.GetComponent<SpriteRenderer>().sortingOrder = sortcount;
            sortcount++;
			SpawnedPlanetList.Add(Spawn);
            if (RandomiseSize)
            {
                float RandomSize = Random.Range(0.5f, 1.1f);
                Spawn.transform.localScale = new Vector3(RandomSize, RandomSize, 1);
            }
        }
    }
}
