using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class ActionCam : MonoBehaviour {
    //Used in the action phase, makes the camera follow the player and zoom in/out

    [Header("Settings:")]
    public int ZoomedOutSize, ZoomedInSize, ZoomInSpeed, ZoomOutSpeed;
    public int ViewDistance = 20;
    public int followSpeed = 50;

    public bool actionPhase = false;
    public Transform PlayerToFollow;

    private Vector3 startPos;
    private Camera cam;
    
    public bool forcedOut = false;

    private void Start()
    {
        cam = GetComponent<Camera>();
        startPos = transform.position;
    }

    void Update () {
        //Zooms camera out and moves to default position
        if (!actionPhase)
        {
            if (cam.orthographicSize < ZoomedOutSize)
            {
                cam.orthographicSize = cam.orthographicSize + (Time.deltaTime*ZoomOutSpeed);
            }

            transform.position = Vector3.Lerp(transform.position, startPos, followSpeed * Time.deltaTime);
            return;
        }
        //Zooms camera in and follows player
        else
        {
            if (cam.orthographicSize > ZoomedInSize && !forcedOut)
            {
                cam.orthographicSize = cam.orthographicSize - (Time.deltaTime*ZoomInSpeed);
            }
            if (forcedOut)
            {
                if (cam.orthographicSize < ZoomedOutSize)
                {
                    cam.orthographicSize = cam.orthographicSize + (Time.deltaTime * ZoomOutSpeed);
                }
            }
            Vector3 CorrectedPosition = new Vector3(PlayerToFollow.position.x, PlayerToFollow.position.y, PlayerToFollow.position.z - ViewDistance);
            transform.position = Vector3.Lerp(transform.position, CorrectedPosition, followSpeed * Time.deltaTime);
        }
	}
}
