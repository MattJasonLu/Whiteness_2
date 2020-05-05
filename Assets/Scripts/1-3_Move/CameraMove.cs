﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	Vector3 offset;
	GameObject player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Use this for initialization
	void Start() { 
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position + offset;
	}
}
