﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulaSpawn : MonoBehaviour {
    //Used for spawning skybox objects such as stars

    public GameObject[] Nebula;

    public int countToSpawn = 10;
    public int MaxStarDistanceXY = 25;

	public List<GameObject> CurrentObjects;

    //Planet player comes from
	public Sprite[] LaunchOptions;
	public SpriteRenderer LaunchPlanetObject;

	public void SpawnMap () 
	{
        //Destroys old objects
		if (CurrentObjects != null) 
		{
			foreach (GameObject obj in CurrentObjects) 
			{
				Destroy (obj);
			}
			CurrentObjects.Clear();
		}

        //Randomise player spawn planet
        LaunchPlanetObject.sprite = LaunchOptions [Random.Range (0, LaunchOptions.Length)];

        //Spawn skybox objects
        for (int count = 0; count < countToSpawn; count++)
        {
            Vector3 RandomPosition = new Vector3(Random.Range(transform.position.x - MaxStarDistanceXY, transform.position.x + MaxStarDistanceXY), Random.Range(transform.position.y - MaxStarDistanceXY, transform.position.y + MaxStarDistanceXY), Random.Range(-2.0f, -1.0f));
            Quaternion RandomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
			CurrentObjects.Add(GameObject.Instantiate(Nebula[Random.Range(0, Nebula.Length)], RandomPosition, RandomRotation));
            float a = Random.Range(1.00f, 100.00f);
            transform.localScale = new Vector3(a, a, transform.localScale.z);
        }
    }
}