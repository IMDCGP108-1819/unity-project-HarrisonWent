﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float rotationSpeed = 0.33f;

	void FixedUpdate () {
        transform.Rotate(Vector3.forward * rotationSpeed);
	}
}
