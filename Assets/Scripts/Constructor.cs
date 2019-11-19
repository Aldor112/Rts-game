using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Constructor : MonoBehaviour {
	
	[Header("Cuartel")]
	public GameObject Cuartel;//todos los datos referidos al cuartel desde costo hasta la UI 
	public int cost_cuartel;//para construirlo y lo que es generado de el 
	public GameObject CuartelUI;
	public GameObject SoldadoUI;
	public GameObject SoldadoMUI;
	[Header("Mina")]// aqui va el prefab de la mina y su costo de construccion junto con la UI
	public GameObject Mina;
	public int cost_Mina;
	public GameObject MinaUI;
	[Header("Fabrica")]//todos los datos referidos a la fabricas desde su costo a la UI junto a la unidad que es generada aqui
	public GameObject Fabrica;
	public int cost_fabrica;
	public GameObject FabricaUI;
	public GameObject TanqueUI;
	private Nodo nodo_selected;//nodo donde sera construido el edificio seleccionado


	public void selec_nodo (Nodo nodo)//todo lo relacionado con el nodo no funciona muy bien no se si por conflictos en el 
	{ 								//script de movimiento y el raycast ya que el nodo no era seleccionado por lo cual 
		if (nodo_selected==nodo) {//la asignacion del nodo de construccion lo hice manual desde el inspector 
			Deselect ();
			return;
		}
	}

	public void Deselect()
	{
		nodo_selected = null;
	}

	public void Construir_Cuartel(Nodo nodo)//aqui se genera el cuartel y aparece la UI para empezar a generar soldados
	{
		GameObject edificio = (GameObject)Instantiate (Cuartel, nodo.transform.position+nodo.offset, Quaternion.identity);
		nodo.edificio = edificio;
		CuartelUI.SetActive (false);
		SoldadoUI.SetActive (true);
		SoldadoMUI.SetActive (true);
	}
	public void Construir_Fabrica (Nodo nodo)//aqui se genera la fabrica asi como habilita la UI para generar la unidad
	{
		GameObject edificio = (GameObject)Instantiate (Fabrica, nodo.transform.position + nodo.offset, Quaternion.identity);
		nodo.edificio = edificio;
		FabricaUI.SetActive (false);
		TanqueUI.SetActive (true);
	}
	public void Construir_Mina(Nodo nodo)//aqui se genera la mina 
	{
		GameObject edificio = (GameObject)Instantiate (Mina, nodo.transform.position + nodo.offset, Quaternion.identity);
		MinaUI.SetActive (false);
	}//al construir el edificio los botones desaparecen ya que solo esta permitido uno de cada tipo
}
		