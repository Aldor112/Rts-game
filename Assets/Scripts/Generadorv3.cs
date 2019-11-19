using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generadorv3 : MonoBehaviour {//script que controla la creacion de los enemigos
	[Header("cositas de posicion")]//Prefab y sitio donde se instancian
	public Transform enemyprefab;
	public Transform Punto_spawn;
	[Header("Temporizadores y el numero de enemigos")]//bueno esta parte es autoexplicativa :v 
	public float Tiempo_entre_oleadas=5f; 
	public float Cuenta_atras=5f;
	public int Numero_Enemigos = 15;

	
	// Update is called once per frame
	void Update () {
		if (Cuenta_atras<=0) {//hay una cuenta atras donde al llegar a cero se llama a una funcion que genera los enemigos
			StartCoroutine(spawn ());
			Cuenta_atras = Tiempo_entre_oleadas;
		}
		Cuenta_atras -= Time.deltaTime;//se reinicia la cuenta atras
		Debug.Log ("tiempo para crear enemigos " + Cuenta_atras);

	}



	IEnumerator spawn(){
		for (int i = 0; i < Numero_Enemigos; i++) {//con este para se instancia el numero de enemigos deseados
		
			Instantiate (enemyprefab, Punto_spawn.position, Punto_spawn.rotation);
			Debug.Log ("creando enemigos");
			yield return new WaitForSeconds (0.5f);//un pequeño retrasito para volver a ejecutar de nuevo las instrucciones en en update
		}

	
	
	}








}
