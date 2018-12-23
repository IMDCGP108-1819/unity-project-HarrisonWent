using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHealth : MonoBehaviour {

    public GameObject particle;

    public void TakeDamage()
    {
        Debug.Log("Passenger ship hit!");
        GameObject.Instantiate(particle, transform.position, transform.rotation);
        GetComponent<Animator>().SetBool("Explode", true);
        Destroy(this.gameObject, 2);
    }
}
