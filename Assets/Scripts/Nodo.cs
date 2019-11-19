using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Nodo : MonoBehaviour {//NADA esto no funciona muy bien mas que para dejar huecos en la funcion para ser asignado los nodos a manos leer el Constructor
	public Vector3 offset;
	public GameObject edificio;
	Constructor constructor;

	void OnMouseDown()
	{ 
		if (edificio !=null) {
			constructor.selec_nodo (this);
			return;
		}
	}


}
