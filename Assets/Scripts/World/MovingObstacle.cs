using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour {

    private bool ReachedTarget = true;
    private GameObject[] Destinations;
    private Transform Target;

    void Start()
    {
        Destinations = GameObject.FindGameObjectsWithTag("Movepoint");
    }

	void Update () {
        if (ReachedTarget)
        {
            Target = Destinations[Random.Range(0, Destinations.Length)].transform;
            ReachedTarget = false;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, Target.position,1 * Time.deltaTime);
            if(Vector3.Distance(transform.position, Target.position) < 1)
            {
                ReachedTarget = true;
            }
        }
	}

}
