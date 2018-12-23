using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Shoot : MonoBehaviour {

    public AudioClip ExplosionSound;
    private ActionCam BattleCam;
    public Aim AimScript;
    private bool Fire = false;
    private int Difficulty = 0;
    private Rigidbody2D myRigid;

    private void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        BattleCam = Camera.main.GetComponent<ActionCam>();
        Difficulty = GameManager.Difficulty;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Fire2") > 0 && !Fire)
        {
            Settings settingObj = GameObject.Find("CarriedOverSettings").GetComponent<Settings>();
            settingObj.immediate = true;
            settingObj.StartCoroutine("switchClip", settingObj.ingameAction);

            GetComponent<AudioSource>().Play();
            StartCoroutine("switchAudioClip");

            Fire = true;
            BattleCam.actionPhase = true;
            AimScript.GetComponent<Transform>().SetParent(null);
            myRigid.gravityScale = 1f;
            if (Difficulty > 0)
            {
                myRigid.velocity = new Vector2(AimScript.VelocityX + Random.Range(0, Difficulty/10), AimScript.VelocityY + Random.Range(0, Difficulty/10));
            }
            else
            {
                myRigid.velocity = new Vector2(AimScript.VelocityX, AimScript.VelocityY);
            }
        }
    }

    IEnumerator switchAudioClip()
    {
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        GetComponent<AudioSource>().clip = ExplosionSound;
    }
}
