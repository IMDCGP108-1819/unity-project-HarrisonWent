using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour {
	
    public bool hasHit = false;
	public GameObject Pointer, particle;
	private GameManager Manager;
	private Transform Target;
	public GameObject MessageObject;
    public Text MessageText;
    public string[] WinMessages;

	void Start ()
	{
		Manager = GameObject.Find ("Manager").GetComponent<GameManager> ();
		InvokeRepeating ("DetectOutOfBounds", 0, 0.5f);;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if(other.tag == "Planet" && !hasHit)
		{
            Debug.Log("Hit planet");
            GetComponent<AudioSource>().Play();
            hasHit = true;
            StartCoroutine(Manager.PrepareForNextWave());
            StartCoroutine(Manager.TimeDown());
            Pointer.SetActive (false);
            other.GetComponent<Planet>().TakeDamage();
            MessageText.text = WinMessages[Random.Range(0, WinMessages.Length)];
			MessageObject.SetActive (true);
        }

        if(other.tag == "Moon")
		{
            Debug.Log("Hit moon");
            GetComponent<AudioSource>().Play();
            StartCoroutine(Manager.TimeDown());
            StartCoroutine("Expand");
            GameObject.Instantiate(particle, transform.position, transform.rotation);
            GetComponent<Animator>().SetBool("Explode", true);
            GetComponent<TrailRenderer>().emitting = false;
            other.GetComponent<ObstacleHealth>().TakeDamage();
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
