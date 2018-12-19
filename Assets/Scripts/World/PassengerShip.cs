using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerShip : MonoBehaviour {

    public Sprite Explosion;

    public void TakeDamage()
    {
        Debug.Log("Passenger ship hit!");
        GetComponent<SpriteRenderer>().sprite = Explosion;

    }
}
