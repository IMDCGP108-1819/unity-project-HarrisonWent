using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour {

    private bool ReachedTarget = true;
    private GameObject[] Destinations;
    private Vector3 Target;

    void Start()
    {
        Destinations = GameObject.FindGameObjectsWithTag("Movepoint");
    }

	void Update () {
        if (ReachedTarget)
        {
            Transform selection = Destinations[Random.Range(0, Destinations.Length)].transform;
            Target = new Vector3(selection.position.x, selection.position.y, Random.Range(-2.0f, 0.0f));
            ReachedTarget = false;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, Target,1 * Time.deltaTime);
            if(Vector3.Distance(transform.position, Target) < 1)
            {
                ReachedTarget = true;
            }
        }
	}

}
