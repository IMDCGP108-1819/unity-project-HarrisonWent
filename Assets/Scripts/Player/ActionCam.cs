using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class ActionCam : MonoBehaviour {
    public int ViewDistance = 20;
    public bool actionPhase = false;
    public Transform PlayerToFollow;
    public int followSpeed = 50;
    private Vector3 startPos;
    private Camera cam;
    public int ZoomedOutSize, ZoomedInSize,ZoomInSpeed,ZoomOutSpeed;
    public bool forcedOut = false;

    private void Start()
    {
        cam = GetComponent<Camera>();
        startPos = transform.position;
    }

    void Update () {
        if (!actionPhase)
        {
            if (cam.orthographicSize < ZoomedOutSize)
            {
                cam.orthographicSize = cam.orthographicSize + (Time.deltaTime*ZoomOutSpeed);
            }

            transform.position = Vector3.Lerp(transform.position, startPos, followSpeed * Time.deltaTime);
            return;
        }
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
