using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombiev2 : MonoBehaviour
{
	public Transform[] points;//arreglo con los puntos que patrullan los zombies
	private int destPoint = 0;
	private NavMeshAgent agent;
	public float rango = 1;
	public float deteccion = 3;//rango de deteccion  de otras unidades hostiles
	private Transform target;//localizacion del objetivo mas cercano
	public string objetivo="player";
	public float vida = 100f;//vida de la unidad

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();//adquisicion del componente navmesh
		agent.autoBraking = false;
		if (target==null) {//si no tiene ningun objetivo llama al modulo de patrulla e invoca repetidamente la funcion de busqueda de obetivos
			GoToNextPoint ();
			InvokeRepeating ("UpdateTarget", 0f, 0.5f);
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (target == null) {//validacion en caso de no tener objetivo
			if (!agent.pathPending && agent.remainingDistance < 0.5f) {//al acercarse a cierta distancia del punto asignado este pasa a dirigirse hacia otro punto
				GoToNextPoint ();

			}

		} else //en caso de tener un objetivo abandona la ruta usual y se dirige hacia el 
		{
			agent.destination = target.position;
		}
		tocar_HQ ();//funcion en caso de tocar el HQ
	}

	void UpdateTarget()
	{//este bloque de instrucciones busca a los enemigos con un tag especifico
	//al detectar verios enemigos busca cual es el mas cercano hacia el y a su vez cual es el que esta dentro de su rango de deteccion
	//cuando lo encuentra modifica su ruta para dirigirse hacia él y en caso de no encontrarlo el objetivo se marca como nulo y sigue su patrulla
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (objetivo);
		float shortdistance = Mathf.Infinity;
		GameObject nearestenemy = null;
		foreach (GameObject player  in enemies) 
		{
			float distanceToEnemy = Vector3.Distance (transform.position, player.transform.position);
			if (distanceToEnemy<shortdistance) 
			{
				shortdistance = distanceToEnemy;
				nearestenemy = player;
			}
		}
		if (nearestenemy != null && shortdistance <= deteccion) {
			target = nearestenemy.transform;
		} else {
			target = null;
		}

	}

	void GoToNextPoint ()
	{//esto funciona que al darle una serie de puntos en el espacio el enemigo se diriga hacia alli
	
		if (points.Length == 0) {
			return;
		}
		agent.destination = points [Random.Range (0, points.Length)].position;//con esta intruccion se elige los puntos aleatoriamente segun el tamaño del arreglo como maximo
		destPoint = (destPoint + 1) % points.Length;
	}

	void tocar_HQ ()
	{//si toca el HQ le quita actualmente 100 de vida y se autodestruye la unidad
		Collider[] collider = Physics.OverlapSphere (transform.position, rango);
		foreach (Collider colliders in collider) {
			if (colliders.tag == "HQ") {
				Player_stats.HP-=100;
				Destroy (gameObject);//esto se explica debido a que los zombies entran en una reaccion quimica que los hace estallar :v
				Debug.Log ("vida restante del hq: " + Player_stats.HP);
			}
		}

	}

	void OnDrawGizmosSelected ()//la funcion que dibuja los diferentes rangos de la unidad
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, rango);
		Gizmos.color = Color.black;
		Gizmos.DrawWireSphere (transform.position, deteccion);
	}
	public void Take_Damage(float amount) //funcion que recibe el daño de la bala y descuenta la vida
	{										//en caso de llegar a cero la unidad muere 
		vida -= amount;
		if (vida<=0f) 
		{
			Destroy (gameObject); 	
		}
	}
}
