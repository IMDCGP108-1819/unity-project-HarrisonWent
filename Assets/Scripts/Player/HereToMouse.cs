using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HereToMouse : MonoBehaviour {

    private Vector2 LastPosition;
    private LineRenderer rendForCurve;
    private bool ChangeAim = false;

    private void Start()
    {
        rendForCurve = GetComponent<LineRenderer>();
        LastPosition = transform.position;
    }

    void Update()
    {
        //if (Input.GetAxisRaw("Fire1") > 0)
        //{
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            ChangeAim = true;
        }
        if (ChangeAim)
        {
            DrawCurve();
        }
        //}
    }

    private void DrawCurve()
    {

        int verts = 2;

        rendForCurve.SetVertexCount(verts);

        Vector3 v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        rendForCurve.SetPosition(0, LastPosition);
        rendForCurve.SetPosition(1, v3);

        ChangeAim = false;


    }
}
