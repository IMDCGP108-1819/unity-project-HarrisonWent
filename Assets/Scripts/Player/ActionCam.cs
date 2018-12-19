using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCam : MonoBehaviour {
    public int ViewDistance = 20;
    public bool actionPhase = false;
    public Transform PlayerToFollow;
    public int followSpeed = 50;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update () {

        if (!actionPhase)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, followSpeed * Time.deltaTime);
            return;
        }
        Vector3 CorrectedPosition = new Vector3(PlayerToFollow.position.x, PlayerToFollow.position.y, PlayerToFollow.position.z - ViewDistance);
        transform.position = Vector3.Lerp(transform.position, CorrectedPosition, followSpeed * Time.deltaTime);
	}
}
