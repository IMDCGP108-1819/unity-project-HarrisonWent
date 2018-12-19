using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    public GameObject PlayerPrefab;
    private int Difficulty = 1;
	public int lives = 3;
	private GameObject Player;
	public GameObject Life3, Life2;

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
			} else if (lives == 0) {
				GameObject.Find ("Manager").GetComponent<GameManager> ().GameOver ();
			}
		}

		if (GameObject.Find ("PathLineRenderer")) {
			Destroy (GameObject.Find ("PathLineRenderer"));
		}

        Difficulty = GameObject.Find("Manager").GetComponent<GameManager>().Difficulty;

        if (Difficulty > 0)
        {
            Physics.gravity = new Vector3(0,Random.Range(-100f, 100f),0);
        }

		Player = GameObject.Instantiate(PlayerPrefab, transform.position, transform.rotation);
		if (GameObject.Find("Main Camera"))
		{
			GameObject.Find("Main Camera").GetComponent<ActionCam>().PlayerToFollow = Player.transform;
			GameObject.Find ("Main Camera").GetComponent<ActionCam> ().actionPhase = false;
		}
		else
		{
			Debug.Log("Cant find main camera! please put one in the scene.");
		}
        
		Debug.Log ("PlayerSpawned");
	}
}
