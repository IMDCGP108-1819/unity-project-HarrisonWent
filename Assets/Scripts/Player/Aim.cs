using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour {
    private Vector2 LastPosition;
    private LineRenderer rendForCurve;
    public float VelocityY,VelocityX = 0.2f;
    private bool ChangeAim = false;
    public float GravityValue = -9.81f;
    public Canvas MovingText;
    public Text Values;

    private void Start()
    {
        rendForCurve = GetComponent<LineRenderer>();
        LastPosition = transform.localPosition;
    }

    void Update () {
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
        if (ChangeAim)
        {
            DrawCurve();
        }
	}

    private void DrawCurve()
    {

        int verts = 10;

        rendForCurve.SetVertexCount(verts);

        Vector2 Velocity = new Vector2(VelocityX,VelocityY);
        Vector2 Gravity = new Vector2(0, GravityValue);

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
