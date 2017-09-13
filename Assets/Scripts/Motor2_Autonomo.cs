using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Motor2_Autonomo : MonoBehaviour {
	public AudioSource audio_hit, audio_level;
    public WheelCollider LDI, LDD, LTI, LTD;
    public float FuerzaDeMotor;
    public float chancleta;
    public float cabrilla;
    public float frenoDeMano;
    public float rotacionMaximaDeLlantas;
    public float FuerzaDeFrenoDeMano;
	public int estaciones = 0;
	double[] est = new double[]{96.5, 177.9, 297.0, 336, 304.8, 212.7, 87.6, 366.4};
	int puntaje = 0;
	int vueltas = 0;
	public UnityEngine.UI.Text text, tiempo;
	
	
	public GameObject target;
	public float angulo, distancia;
	public Vector3 productoCruz;
	//private float[] targetPosX = new float[]{240, 281, 218, 275, 432, 413, 486, 263};
	//private float[] targetPosZ = new float[]{110, 176, 298, 326, 322, 217, 116, 66};
	private float[] targetPosX = new float[]{240, 288, 218, 275, 432, 425, 433, 486, 498, 263};
	private float[] targetPosZ = new float[]{110, 176, 298, 326, 322, 250, 178, 116, 93, 66};
	private int targetsHit = 0;

	private float time_init = 0f;
	
	
	// Use this for initialization
	void Start () {
		//time_init = 0f;
	}
	
	// Update is called once per frame
	void Update () {

			angulo = Vector3.Angle(transform.forward, target.transform.position - transform.position);
			productoCruz = Vector3.Cross(transform.forward, target.transform.position - transform.position);
			cabrilla = productoCruz.y > 0 ? 1 : -1;
			if(angulo<5){
				cabrilla=0;
			}
			if(angulo>75){
				chancleta=0;
				frenoDeMano=1;
			}else{
				chancleta=1;
				frenoDeMano=0;
			}			
			distancia = Vector3.Distance (transform.position, target.transform.position);
			if(distancia < 5.0){
				targetsHit = targetsHit + 1;
				target.transform.position = new Vector3(targetPosX[targetsHit%10], -0.18f, targetPosZ[targetsHit%10]);
			}
	
		
		
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
					audio_level.Play();
				}
			}else if(i>=0 && i<=2){
				if(this.transform.position.z >=est[i] && estaciones==i){
					estaciones = (i+1)%8;
					puntaje = puntaje + 100;
					audio_level.Play();
					if(estaciones==1){
						vueltas = vueltas + 1;						
						if(vueltas > 1){
							time_init=0f;
							puntaje = puntaje + 100;
						}
					}
				}
			}else if(i>=4 && i<=6){
				if(this.transform.position.z <=est[i] && estaciones==i){
					estaciones = (i+1)%8;
					puntaje = puntaje + 100;
					audio_level.Play();
				}
			}else if(i==7){
				if(this.transform.position.x <=est[i] && estaciones==i){
					estaciones = (i+1)%8;
					puntaje = puntaje + 100;
					audio_level.Play();
				}
			}
		}	
		
        text.text = "Puntaje: " + puntaje + " Vueltas: " + vueltas;
				
        time_init += Time.deltaTime;
		int seg = (int)(time_init%60);
		int min = (int)(time_init/60)%60;
		int hours = (int)(time_init/3600)%24;
		string timer_string = string.Format("{0:0}:{1:00}:{2:00}", hours, min, seg);
		tiempo.text = timer_string;
    }
	
	void OnCollisionEnter (Collision col)
    {
		if(col.gameObject.name.Contains("Bala")){
			puntaje = puntaje - 10;		
			audio_hit.Play();
		}
    }
}
