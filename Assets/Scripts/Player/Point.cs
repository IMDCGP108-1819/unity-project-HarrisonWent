using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {

    public Transform Target;

    void Start()
	{
		StartCoroutine (FindTarget ());
    }

	private IEnumerator FindTarget()
	{
		while (Target == null) 
		{
			yield return 0;
			Target = GameObject.FindWithTag ("Planet").transform;
		}
	}

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
