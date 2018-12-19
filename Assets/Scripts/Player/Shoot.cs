using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    private ActionCam BattleCam;
    public Aim AimScript;
    private bool Fire = false;
    private int Difficulty = 0;

    private void Start()
    {
        BattleCam = Camera.main.GetComponent<ActionCam>();
        Difficulty = GameObject.Find("Manager").GetComponent<GameManager>().Difficulty;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Fire2") > 0 && !Fire)
        {
            Fire = true;
            BattleCam.actionPhase = true;
            AimScript.GetComponent<Transform>().SetParent(null);
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            if (Difficulty > 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(AimScript.VelocityX + Random.Range(0, Difficulty/10), AimScript.VelocityY + Random.Range(0, Difficulty/10));
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(AimScript.VelocityX, AimScript.VelocityY);
            }
        }
    }
}
