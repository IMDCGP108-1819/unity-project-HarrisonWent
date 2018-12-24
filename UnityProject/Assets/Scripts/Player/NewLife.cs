using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLife : MonoBehaviour {
    //Used as poer up, increases remaining lives

    //Particle spawned on death
    public GameObject Particle;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (SpawnPlayer.lives != 3)
            {
                Debug.Log("+ Life");

                //Increases life and adds icon back to health UI
                GameObject.Find("PlayerSpawn").GetComponent<SpawnPlayer>().IncreaseLife();

                Instantiate(Particle, transform.position, transform.rotation);
            }
            Destroy(this.gameObject);
        }

    }
}
