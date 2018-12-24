using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SpawnPlayer : MonoBehaviour {
    //Spawns player at start of wave

    private GameObject Player;

    public GameObject PlayerPrefab;
    public AudioMixer SoundMixer;

    public GameObject Life3, Life2;
    public static int lives = 3;

    private void Start()
    {
        //Resets remaining lives
        lives = 3;
    }
    
    public void Spawn (bool FailedToTarget) 
	{

        //Destroys previous player
		if (Player != null) {
			Destroy (Player);
            Settings settingObj = GameObject.Find("CarriedOverSettings").GetComponent<Settings>();

            //Switches to out of combat music
            settingObj.immediate = false;
            settingObj.StartCoroutine("switchClip", settingObj.ingameNoAction);
        }

        //Removes life if failed to hit planet
		if (FailedToTarget) {
			lives -= 1;

			if (lives == 2) {
				Life3.SetActive (false);	
			} else if (lives == 1) {
				Life2.SetActive (false);
			}
		}

        //Destroys previous path
		if (GameObject.Find ("PathLineRenderer")) {
			Destroy (GameObject.Find ("PathLineRenderer"));
		}        

        //Randomizes gravity when harder than easy, makes it feel harder
        if (GameManager.Difficulty > 0)
        {
            Physics.gravity = new Vector3(0,Random.Range(-100f, 100f),0);
        }

        //Resets time in case the player respawns before setting it back to 1
        Time.timeScale = 1;

		Player = GameObject.Instantiate(PlayerPrefab, transform.position, transform.rotation);

        //Resets camera mode
        GameObject cam;
        if (cam = GameObject.Find("Main Camera"))
		{
            SoundMixer.SetFloat("MasterVolume", 0);
            cam.GetComponent<ActionCam>().PlayerToFollow = Player.transform;
			cam.GetComponent<ActionCam> ().actionPhase = false;
		}
		else
		{
			Debug.Log("Cant find main camera! please put one in the scene.");
		}
        
	}

    public void IncreaseLife()
    {
        lives += 1;
        if (lives == 3)
        {
            Life3.SetActive(true);
        }
        else if (lives == 2)
        {
            Life2.SetActive(true);
        }
    }
}
