using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Shoot : MonoBehaviour {
    //Used to go to shooting phase from aiming phase

    public AudioClip ExplosionSound;
    public Aim AimScript;

    private ActionCam BattleCam;
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
        //On right click
        if (Input.GetAxisRaw("Fire2") > 0 && !Fire)
        {
            Settings settingObj = GameObject.Find("CarriedOverSettings").GetComponent<Settings>();

            //Switches music to combat
            settingObj.immediate = true;
            settingObj.StartCoroutine("switchClip", settingObj.ingameAction);

            //Launch sound
            GetComponent<AudioSource>().Play();
            StartCoroutine("switchAudioClip");


            Fire = true;
            BattleCam.actionPhase = true;
            AimScript.GetComponent<Transform>().SetParent(null);
            myRigid.gravityScale = 1f;

            //Launches projectile using aim scripts velocity
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


    //Switches to combat sound from launching sound
    IEnumerator switchAudioClip()
    {
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        GetComponent<AudioSource>().clip = ExplosionSound;
    }
}
