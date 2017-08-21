using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Motor2 : MonoBehaviour {
    public WheelCollider LDI, LDD, LTI, LTD;
    public float FuerzaDeMotor;
    public float chancleta;
    public float cabrilla;
    public float frenoDeMano;
    public float rotacionMaximaDeLlantas;
    public float FuerzaDeFrenoDeMano;
	private int estaciones = 0;
	double[] est = new double[]{96.5, 177.9, 297.0, 336, 304.8, 212.7, 87.6, 366.4};
	int puntaje = 0;
	int vueltas = 0;
	public UnityEngine.UI.Text text;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.D))
        {
            cabrilla = 1;
        }else if (Input.GetKey(KeyCode.A))
        {
            cabrilla = -1;
        } else
		{
			cabrilla = 0;
		}
        if (Input.GetKey(KeyCode.W))
        {
            chancleta = 1;
        } else if (Input.GetKey(KeyCode.S))
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
		
		for(int i=0; i<=7; i++){
			if(i==3){
				if(this.transform.position.x >=est[i] && estaciones==i){
					estaciones = (i+1)%8;
					puntaje = puntaje + 100;
				}
			}else if(i>=0 && i<=2){
				if(this.transform.position.z >=est[i] && estaciones==i){
					estaciones = (i+1)%8;
					puntaje = puntaje + 100;
					if(estaciones==1){
						vueltas = vueltas + 1;
						if(vueltas > 1){
							puntaje = puntaje + 100;
						}
					}
				}
			}else if(i>=4 && i<=6){
				if(this.transform.position.z <=est[i] && estaciones==i){
					estaciones = (i+1)%8;
					puntaje = puntaje + 100;
				}
			}else if(i==7){
				if(this.transform.position.x <=est[i] && estaciones==i){
					estaciones = (i+1)%8;
					puntaje = puntaje + 100;
				}
			}
		}	
		
        text.text = "Puntaje: " + puntaje + " Vueltas: " + vueltas;
        
    }
	
	void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.name.Contains("Bala")){
			puntaje = puntaje - 10;			
		}
    }
}
