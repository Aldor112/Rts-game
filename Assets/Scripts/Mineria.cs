using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineria : MonoBehaviour {//este script empieza a funcionar al ser instanciada la mina

	public int Generar_Dinero = 0;//dinero que genera la mina
	public float Tiempo_inicial=10f;//tiempo que tarda en generar mas dinero
	private float tiempo_G=10f;//tiempo que tarda en generara mas dinero, pero inicialmente si se modifica el la variable tiempo_iinicial lo disminuye

	// Update is called once per frame
	void Update () 
	{
		if (tiempo_G<=0){//cuando llega a cero se le asigna al jugador mas dinero
			Player_stats.Money += Generar_Dinero;//asigna el dinero 
			tiempo_G = Tiempo_inicial;//asigna el tiempo inicial como nuevo tiempo a generar dinero
			Debug.Log ("dinero generado");
			}
		tiempo_G -= Time.deltaTime;//contador del dinero en tiempo de ejecucion
		Debug.Log ("tiempo para dinero: " + tiempo_G);
	}
}
