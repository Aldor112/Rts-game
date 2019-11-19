using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Control_Juego : MonoBehaviour {
	[Header("Cosas del tiempo y su UI")]
	public GameObject Finalizar_UI;//la UI del boton que hace finalizar el juego
	public Text Cuenta_atras;// la UI que representa la cuenta atras. NO funciona, por alguna razon no se muestra
	public float Reloj = 300f;// el tiempo hasta que se habilite la UI del boton, son 5 minutos
	[Header("Cosas de la finalizacion y sus efectos")]
	public GameObject Efecto_explosion;//efecto de una explosion
	public Transform portal_enemigo;//posicion del portal donde se generan los enemigos
	public GameObject FinalUI;//UI pero del fin del jueg OJO no confundir con finalizar_UI

	// Update is called once per frame
	void Update () 
	{
		if (Reloj<=0 || Input.GetKeyDown(KeyCode.Escape)) //aqui se realiza la cuenta atras del reloj
		{												//el key code es para facilitar las pruebas
			Finalizar_UI.SetActive (true);
		}
		Reloj -= Time.deltaTime;//cuenta atras del reloj
		Reloj = Mathf.Clamp (Reloj, 0f, Mathf.Infinity);//tratamiento de los numeros del reloj para que no siga bajando hasta menos infinito
		Cuenta_atras.text = string.Format ("000",Reloj);//UI de la cuenta atras, por ahora no se esta visualizando bien
	
	}
	public void Finalizar()
	{	//Instanciacion del efecto de la explosion, un sin sentido que hice debido a que al acabar el juego el timeScale se detiene :v
		GameObject Effect = (GameObject)Instantiate (Efecto_explosion, portal_enemigo.position, portal_enemigo.rotation);
		activar ();
	}

	public void activar()// al activar el boton de finalizar se activa la UI de fin del juego que a su vez detiene el tiempo del juego 
	{
		FinalUI.SetActive (!FinalUI.activeSelf);
		if (FinalUI.activeSelf) {
			Time.timeScale = 0f;
		} else 
		{
			Time.timeScale = 1f;
		}

	}
	public void Reiniciar()//funcion asignada a un boton que reinicia la escena 
	{					//revisar porque creo que al reiniciar el tiempo no vuelve a circular a pesar de la validacion de arriba
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void Cerrar()//funcion asignada a un boton para que cierre la aplicacion
	{
		Application.Quit();
	}
}
