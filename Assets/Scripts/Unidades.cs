using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidades : MonoBehaviour {
	[Header("Cuartel")]
	public GameObject Soldado;//prefab del soldado
	public int Precio_Soldado=0;//precio de construccion del soldado
	public GameObject Soldado_Misiles;//prefab del soldado con misil
	public int Precio_Soldado_M=0;//precio del soldado con misil
	public Transform Infanteria;//punto donde son generados los soldados
	[Header("Fabrica")]
	public GameObject Tanque;//prefab del tanque
	public Transform Mecanizada;//punto de generacion de la fabrica
	public int Precio_Tanque=0;//precio de construcion del soldado
	
	public void Crear_Soldado()
	{
		Instantiate (Soldado,Infanteria.position,Infanteria.rotation);//instanciacion del soldado y el cobro de su construccion
		Player_stats.Money -= Precio_Soldado;
	}
	public void Crear_Soldado_M()
	{
		Instantiate (Soldado_Misiles, Infanteria.position,Infanteria.rotation);//instanciacion del soldado de misiles y el cobro de su construccion
		Player_stats.Money -= Precio_Soldado_M;
	}
	public void Crear_Tanque()
	{//instanciacion del tanque y el cobro de su contruccion
		Instantiate (Tanque, Mecanizada.position,Mecanizada.rotation);
		Player_stats.Money -= Precio_Tanque;
	}


}
