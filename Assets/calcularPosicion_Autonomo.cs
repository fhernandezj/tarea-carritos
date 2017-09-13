using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class calcularPosicion_Autonomo : MonoBehaviour {

	public GameObject c1;
	public GameObject c2;
	public UnityEngine.UI.Text pos1, pos2;
	float[] posiciones = new float[]{96.5f, 177.9f, 297.0f, 336f, 304.8f, 212.7f, 87.6f, 366.4f};
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int est1 = c1.GetComponent<Motor1>().estaciones;
		int est2 = c2.GetComponent<Motor2_Autonomo>().estaciones;
		if(est1>est2){
			pos1.text = "Posición: 1/2";
			pos2.text = "Posición: 2/2";
		}else if(est2>est1){
			pos2.text = "Posición: 1/2";
			pos1.text = "Posición: 2/2";
		}else{
			float dist1=0, dist2=0;
			if(est1==3 || est1==7){
				dist1 = posiciones[est1%8] - c1.transform.position.x;
				dist2 = posiciones[est1%8] - c2.transform.position.x;
			}else{
				dist1 = posiciones[est1%8] - c1.transform.position.z;
				dist2 = posiciones[est1%8] - c2.transform.position.z;
			}
			if(dist1<=dist2){
				pos1.text = "Posición: 1/2";
				pos2.text = "Posición: 2/2";
			}else{
				pos2.text = "Posición: 1/2";
				pos1.text = "Posición: 2/2";
			}
		}
	}
}
