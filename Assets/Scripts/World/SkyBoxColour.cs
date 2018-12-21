using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxColour : MonoBehaviour {

    private Color ColourGen;
    public bool cycle = false;

    private void Start()
    {
        if (cycle)
        {
            InvokeRepeating("newColour", 0, 5);
        }
    }
    public void newColour()
    {
        ColourGen = new Color(Random.Range(0.0f, 0.25f), Random.Range(0.0f, 0.25f), Random.Range(0.0f, 0.25f));
    }

    private void Update()
    {
        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, ColourGen, Time.deltaTime);
    }
}
