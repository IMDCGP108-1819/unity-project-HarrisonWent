using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {
    //Used on arrow, points to target planet

    public Transform Target;

    void Start()
	{
		StartCoroutine (FindTarget ());
    }

	private IEnumerator FindTarget()
	{
        //Waits for target to be in scene then sets it as target
		while (Target == null) 
		{
			yield return 0;
			Target = GameObject.FindWithTag ("Planet").transform;
		}
	}

    //Points to target
	void Update () 
	{
        if (Target == null)
		{
            return;
        }
        else
		{
            transform.LookAt(Target.position);
            transform.Rotate(0, 90, 0);
        }
	}
}
