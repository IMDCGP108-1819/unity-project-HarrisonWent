using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class ObstacleHealth : MonoBehaviour {
    //Used to destroy moving obstacles

    public GameObject particle;

    public void TakeDamage()
    {
        GameObject.Instantiate(particle, transform.position, transform.rotation);

        GetComponent<Animator>().SetBool("Explode", true);

        Destroy(this.gameObject, 2);
    }
}
