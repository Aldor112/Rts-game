using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movimiento : MonoBehaviour {
	private Transform target;//localizacion del objetivo
	public NavMeshAgent agent;//variable necesaria para el navmesh
	[Header("Configuracion basica de las unidades")] 
	public float colision=1f;//colision se refiere al area alrededor del objeto que es considerada como tocable
	public float rango=4f;//rango de disparos
	public string EnemyTag = "enemigo";//el tag del enemigo
	public float velocidad=10f;//velocidad de movimiento
	[Header("Configuracion de sus disparos")]
	public GameObject _bala;//el prefab de la bala
	public float firecountdown = 1f;//tiempo entre disparos
	public float firerate = 1f;//cantidad de disparos 
	public Transform firepoint;//sitio de donde se instancian las balas
	
	// Use this for initialization
	void Start () {//aqui se autoasigna el objeto el componente de navmesh
		agent = GetComponent<NavMeshAgent> ();
		InvokeRepeating ("Update_Objetivo", 0f, 0.10f);//esto invoca repetidamente la funcion que busca enemigos
	}
	
	// Update is called once per frame
	void FixedUpdate () 
		if (Input.GetMouseButtonDown(0)) {//estas instrucciones tratan de que al hacer click con el raton en algun sitio
			RaycastHit hit;		//del mapa la unidad proceda a moverse hacia ese lugar
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,100)) 
			{
			agent.destination = hit.point; //asignacion de la instruccion de moverse al sitio donde golpeo el raycast
			}
		}
		if (target==null) {//este bloque de instruccion esta enfocado a la busqueda del enemigo
			return;
		}
		LockOnTarget ();//si encuentra a un enemigo dentro de su rango empieza a llamar modulos que estan encargados del comportamiento
		golpe_recibido();
		if (firecountdown<=0f) //aqui si llega a 0 le dice que dispare y reinicia la cuenta atras
		{
			shoot ();
			firecountdown = 1f / firerate;
		}
		firecountdown -= Time.deltaTime;
	}



	void golpe_recibido() //si el enemigo toca al soldado este es destruido totalmente
	{//esto es explicado en el lore debido a que los enemigos contienen acidos y virus que literalmente destruyen la carne y el acero
		Collider[] collider = Physics.OverlapSphere (transform.position, colision);
		foreach (Collider colliders in collider) {
			if (colliders.tag == EnemyTag) {
				Destroy (gameObject);
			}

		}

	}

	void OnDrawGizmosSelected ()
	{   //rango de la colision
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, colision);
		//rango para fijar el objetivo
		Gizmos.color = Color.black;
		Gizmos.DrawWireSphere (transform.position, rango);
	}
	//parte que servira para disparar

	void Update_Objetivo()//aqui se busca dentro del rango de deteccion enemigos para ser fijados priorizando al enemigo mas cercano
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
		float shortdistance = Mathf.Infinity;
		GameObject nearestenemy = null;
		foreach (GameObject enemigo in enemies) 
		{
		
			float distancetoenemy = Vector3.Distance (transform.position, enemigo.transform.position);
			if (distancetoenemy<shortdistance) {
				shortdistance = distancetoenemy;
				nearestenemy = enemigo;

			}
		}
		if (nearestenemy != null && shortdistance <= rango) {
			target = nearestenemy.transform;
		} else {
			target = null;
		}

	}

	void LockOnTarget()
	{//calculos para hacer que el objeto rote hacia el enemigo, de esta forma siempre mira hacia el enemigo 
		Vector3 dir = target.position - transform.position;
		Quaternion lookrotation = Quaternion.LookRotation (dir);
		Vector3 Rotation = Quaternion.Lerp (transform.rotation, lookrotation, Time.deltaTime * velocidad).eulerAngles;
		transform.rotation = Quaternion.Euler (0f,Rotation.y,0f);
	}

	void shoot()//creacion de la bala donde es pasado por parametros al objetivo fijado
	{
		GameObject balaGO = (GameObject)Instantiate (_bala, firepoint.position, firepoint.rotation);
		Bala bala = balaGO.GetComponent<Bala> ();
		if (bala !=null) {
			bala.Seek (target);
		}
	}

}
