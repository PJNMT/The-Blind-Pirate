﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipcontroller : MonoBehaviour
{
	public float moveSpeed = 20f;
	public float turnSpeed = 50f;
	
	// Update is called once per frame
    void Update()
    {

	    if (Input.GetKey(KeyCode.UpArrow)) moveSpeed += 1f; //on augmente la vitesse ou on la baisse
	    if (Input.GetKey(KeyCode.DownArrow)) moveSpeed -= 1f;
	    if(Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        if(Input.GetKey(KeyCode.RightArrow)) transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime); //on tourne en f° du temps et non de l'update
	    if (Input.GetKey(KeyCode.Space)) moveSpeed += 1f;

	    if (moveSpeed < 0f) moveSpeed = 0f;
	    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); //on avance en f° du temps
	    
	   
        
        		
    }
}
