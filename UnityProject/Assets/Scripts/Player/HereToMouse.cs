using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HereToMouse : MonoBehaviour {
    //Used to draw straight line to mouse position

    private Vector2 LastPosition;
    private LineRenderer rendForCurve;

    private void Start()
    {
        rendForCurve = GetComponent<LineRenderer>();
        LastPosition = transform.position;
    }

    void Update()
    {
        //While left click
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            DrawLine();
        }
    }

    //Draws straight line
    private void DrawLine()
    {
        int verts = 2;

        rendForCurve.positionCount = verts;

        Vector3 v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        rendForCurve.SetPosition(0, LastPosition);
        rendForCurve.SetPosition(1, v3);
    }
}
