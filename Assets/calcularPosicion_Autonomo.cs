using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class calcularPosicion_Autonomo : MonoBehaviour {

	public GameObject c1;
	public GameObject c2;
	public UnityEngine.UI.Text pos1, pos2, res1, res2;
	float[] posiciones = new float[]{96.5f, 177.9f, 297.0f, 336f, 304.8f, 212.7f, 87.6f, 366.4f};
	private bool sent = false;
	public int est_ganar = 9;
	
	[SerializeField]
    private string BASE_URL = "https://docs.google.com/a/uninorte.edu.co/forms/d/e/1FAIpQLSedyB_RucEm2VZOrf7mu8z1DW_AT1XhdzppR5ZSQaSQgHk0Xw/formResponse";

    IEnumerator Post(string jugador, string tiempo, string puntaje) {
        WWWForm form = new WWWForm();
        form.AddField("entry.82908517", jugador);
        form.AddField("entry.326075018", tiempo);
        form.AddField("entry.848506399", puntaje);
        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URL, rawData);
        yield return www;
    }
	
    public void Send() {
        StartCoroutine(Post("JugadorTest", "0:01:35", "950"));
    }
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int est1 = c1.GetComponent<Motor1>().checkpoints;
		int est2 = c2.GetComponent<Motor2_Autonomo>().checkpoints;
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
		
		if(est1==est_ganar && !sent){
			sent = true;
			res1.text = "YOU WIN";
			res2.text = "YOU LOOSE";
			string puntaje = "" + c1.GetComponent<Motor1>().puntaje;
			string tiempo = c1.GetComponent<Motor1>().timer_string;
			StartCoroutine(Post("Jugador1", tiempo, puntaje));
		}
		
		if(est2==est_ganar && !sent){
			sent = true;
			res2.text = "YOU WIN";
			res1.text = "YOU LOOSE";
			string puntaje = "" + c2.GetComponent<Motor2_Autonomo>().puntaje;
			string tiempo = c2.GetComponent<Motor2_Autonomo>().timer_string;
			StartCoroutine(Post("Jugador2", tiempo, puntaje));
		}
	}
}
