using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxColour : MonoBehaviour {
    //Used to change skybox colour

    private Color ColourGen;
    public bool cycle = false;

    private void Start()
    {
        //if cycle through colours automaticaly
        if (cycle)
        {
            InvokeRepeating("newColour", 0, 5);
        }
    }

    //Generate new dim colour (excludes colours over 25% saturation)
    public void newColour()
    {
        ColourGen = new Color(Random.Range(0.0f, 0.25f), Random.Range(0.0f, 0.25f), Random.Range(0.0f, 0.25f));
    }

    //Fades to new colour
    private void Update()
    {
        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, ColourGen, Time.deltaTime);
    }
}
