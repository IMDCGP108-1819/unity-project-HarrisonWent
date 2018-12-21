using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    private GameObject Player;
    public GameObject PlayerPrefab;
    public GameObject Life3, Life2;

    public static int lives = 3;

    private GameManager ManagerScript;

    private void Start()
    {
        ManagerScript = GameObject.Find("Manager").GetComponent<GameManager>();
    }
    
    public void Spawn (bool FailedToTarget) 
	{
		if (Player != null) {
			Destroy (Player);
		}

		if (FailedToTarget) {
			lives -= 1;

			if (lives == 2) {
				Life3.SetActive (false);	
			} else if (lives == 1) {
				Life2.SetActive (false);
			}
		}

		if (GameObject.Find ("PathLineRenderer")) {
			Destroy (GameObject.Find ("PathLineRenderer"));
		}        

        if (GameManager.Difficulty > 0)
        {
            Physics.gravity = new Vector3(0,Random.Range(-100f, 100f),0);
        }

		Player = GameObject.Instantiate(PlayerPrefab, transform.position, transform.rotation);

        GameObject cam;
        if (cam = GameObject.Find("Main Camera"))
		{
			cam.GetComponent<ActionCam>().PlayerToFollow = Player.transform;
			cam.GetComponent<ActionCam> ().actionPhase = false;
		}
		else
		{
			Debug.Log("Cant find main camera! please put one in the scene.");
		}
        
	}
}
