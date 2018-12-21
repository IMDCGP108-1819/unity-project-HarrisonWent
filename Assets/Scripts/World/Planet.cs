using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Planet : MonoBehaviour {

    public GameObject Explos1, Explos2;

	public void TakeDamage()
    {
        Debug.Log("Planet target hit!");
        GetComponent<Animator>().SetBool("Explode", true);
        if (transform.localScale.x <= 0.0f) { return; }
        StartCoroutine("TimeDown");
        StartCoroutine("Expand");
    }

    IEnumerator Expand()
    {        
        float doUntil = transform.localScale.x - 0.25f;
        for (float a = transform.localScale.x; a > doUntil; a-=0.1f)
        {
            yield return null;
            GameObject.Instantiate(Explos1, transform.position, transform.rotation);
            GameObject.Instantiate(Explos2, transform.position, transform.rotation);
            transform.localScale = new Vector3(a, a, transform.localScale.z);
        }
    }

    IEnumerator TimeDown()
    {
        for(float a = 1.00f; a > 0.25f; a-=0.05f)
        {
            Time.timeScale = a;
            yield return null;
        }

        yield return new WaitForSeconds (0.5f);

        for (float a = 0.25f; a < 1.00f; a += 0.05f)
        {
            Time.timeScale = a;
            yield return null;
        }

    }
}
