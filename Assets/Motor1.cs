﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor1 : MonoBehaviour {
    public WheelCollider LDI, LDD, LTI, LTD;
    public float FuerzaDeMotor;
    public float chancleta;
    public float cabrilla;
    public float frenoDeMano;
    public float rotacionMaximaDeLlantas;
    public float FuerzaDeFrenoDeMano;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cabrilla = 1;
        }else if (Input.GetKey(KeyCode.LeftArrow))
        {
            cabrilla = -1;
        } else
		{
			cabrilla = 0;
		}
        if (Input.GetKey(KeyCode.UpArrow))
        {
            chancleta = 1;
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            chancleta = -1;
		}else{
			chancleta = 0;
		}
        frenoDeMano = Input.GetAxisRaw("Jump");
        LDI.motorTorque = chancleta * FuerzaDeMotor * Time.deltaTime;
        LDD.motorTorque = chancleta * FuerzaDeMotor * Time.deltaTime;
        LDD.steerAngle = cabrilla * rotacionMaximaDeLlantas;
        LDI.steerAngle = cabrilla * rotacionMaximaDeLlantas;
        
        if (frenoDeMano > 0f)
        {
            LTI.brakeTorque = FuerzaDeFrenoDeMano;
            LTD.brakeTorque = FuerzaDeFrenoDeMano;
        }
        else {
            LTI.brakeTorque = 0f;
            LTD.brakeTorque = 0f;
        }
        
    }
}
