using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour {
	//Used for collisions and events for player asteroid projectile
        
    public bool hasHit = false;
	public GameObject Pointer, particle;

    [Header("Player UI:")]
    public GameObject MessageObject;
    public Text MessageText;
    public string[] WinMessages;

    private GameManager Manager;
	private Transform Target;


	void Start ()
	{
		Manager = GameObject.Find ("Manager").GetComponent<GameManager> ();
		InvokeRepeating ("DetectOutOfBounds", 0, 0.5f);;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if(other.tag == "Planet" && !hasHit)
		{
            //Collision sound
            GetComponent<AudioSource>().Play();

            hasHit = true;

            //Sets up for new wave/ respawn target and player
            StartCoroutine(Manager.PrepareForNextWave());

            //Slows time down
            StartCoroutine(Manager.TimeDown());

            //Disables arrow which points at planet target
            Pointer.SetActive (false);

            //Destroys planet
            other.GetComponent<Planet>().TakeDamage();

            MessageText.text = WinMessages[Random.Range(0, WinMessages.Length)];
			MessageObject.SetActive (true);
        }

        if(other.tag == "Moon")
		{
            //Collision sound
            GetComponent<AudioSource>().Play();

            //Slows time down
            StartCoroutine(Manager.TimeDown());

            //Scales up player object
            StartCoroutine("Expand");
            
            GameObject.Instantiate(particle, transform.position, transform.rotation);

            //Plays explode player animation
            GetComponent<Animator>().SetBool("Explode", true);

            //Stops player trail from spawning
            GetComponent<TrailRenderer>().emitting = false;

            //Destroys the obstacle
            other.GetComponent<ObstacleHealth>().TakeDamage();
        }

        //Prevents additional life from slowing the player down
        if (other.tag != "PowerUp")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x / 3, GetComponent<Rigidbody2D>().velocity.y / 3);
        }
    }


    //Detects when player is out of the level/ Impossible to hit target
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

    //Scales the player up
    IEnumerator Expand()
    {
        float doUntil = transform.localScale.x + 1f;
        for (float a = transform.localScale.x; a < doUntil; a += 0.1f)
        {
            yield return null;
            transform.localScale = new Vector3(a, a, transform.localScale.z);
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        Manager.GameOver();
    }
}
