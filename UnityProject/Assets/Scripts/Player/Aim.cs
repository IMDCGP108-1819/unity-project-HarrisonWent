using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour {
    //Used for aiming the asteroid

    [Header("Settings:")]
    public float VelocityY, VelocityX = 0.2f;
    public float GravityValue = -9.81f;

    private Vector2 LastPosition;
    private LineRenderer rendForCurve;    
    private bool ChangeAim = false;

    [Header("Objects:")]
    public Canvas MovingText;
    public Text Values;

    private void Start()
    {
        rendForCurve = GetComponent<LineRenderer>();
        if (GameManager.Difficulty > 0)
        {
            rendForCurve.enabled = false;
        }
        LastPosition = transform.localPosition;
    }

    void Update () {
        //Updates path when left click
        if(Input.GetAxisRaw("Fire1") > 0)
        {
            Vector3 v3 = Input.mousePosition;
            v3.z = 10.0f;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            VelocityX = (Mathf.Abs((transform.position - v3).x))*2;
            VelocityY = (Mathf.Abs((transform.position - v3).y))*2;
            if (VelocityX > 20) { VelocityX = 20; }
            if (VelocityY > 20) { VelocityY = 20; }
            MovingText.transform.position = (transform.position + v3)/2;
            Values.text = ("X" + VelocityX + " : Y" + VelocityY);
            ChangeAim = true;
        }
        //Draws path when left click
        if (ChangeAim)
        {
            DrawCurve();
        }
	}

    //Draws trajectory of asteroid
    private void DrawCurve()
    {
        int verts = 0;
        switch(GameManager.Difficulty)
        {
            case 0:
                //Easy gives full path
                verts = 20;
                break;
            case 1:
                //Normal gives half path
                verts = 10;
                break;
            case 2:
                //Hard no path is drawn
                return;
        }

        rendForCurve.positionCount = verts;

        Vector2 Velocity = new Vector2(VelocityX,VelocityY);
        Vector2 Gravity = new Vector2(0, GravityValue);

        //Sets poistion of points in path
        for (int i = 0; i < verts; i++)
        {
            rendForCurve.SetPosition(i, new Vector3(LastPosition.x, LastPosition.y, 0));
            Velocity = Velocity + Gravity * 0.1f;
            LastPosition = LastPosition + Velocity * 0.1f;
        }
        
        ChangeAim = false;
        LastPosition = new Vector2(0, 0);
        
    }
}
