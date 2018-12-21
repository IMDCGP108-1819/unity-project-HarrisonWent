using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour {
	
    public bool hasHit = false;
	public GameObject Pointer;
	private GameManager Manager;
	private Transform Target;
	public GameObject MessageObject;

	void Start ()
	{
		Manager = GameObject.Find ("Manager").GetComponent<GameManager> ();
		InvokeRepeating ("DetectOutOfBounds", 0, 0.5f);;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if(other.tag == "Planet" && !hasHit)
		{
            hasHit = true;
			Pointer.SetActive (false);
            other.GetComponent<Planet>().TakeDamage();
			MessageObject.SetActive (true);
			StartCoroutine(Manager.PrepareForNextWave ());
        }

        if(other.tag == "Passenger")
		{
            other.GetComponent<PassengerShip>().TakeDamage();
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x / 3, GetComponent<Rigidbody2D>().velocity.y / 3);
    }

	void DetectOutOfBounds()
	{
        
		if (Target == null) 
		{
			Target = GameObject.FindWithTag ("Planet").transform;
			return;
		}

		if ((transform.position.x > Target.position.x || transform.position.y<Target.position.y-20) && !hasHit) 
		{
            if(SpawnPlayer.lives == 1)
            {
                MessageObject.SetActive(true);
                MessageObject.GetComponent<Text>().text = "GAME OVER!";
                Camera.main.GetComponent<ActionCam>().forcedOut = true;
                Invoke("GameOver",2);
            }
            else
            {
                GameObject.Find("PlayerSpawn").GetComponent<SpawnPlayer>().Spawn(true);
            }					

		}
			
	}

    private void GameOver()
    {
        Debug.Log("Game Over");
        Manager.GameOver();
    }
}
